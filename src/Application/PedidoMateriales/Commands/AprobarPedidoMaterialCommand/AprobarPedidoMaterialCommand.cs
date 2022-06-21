using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Constants;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.PedidoMateriales.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.PedidoMateriales.Commands.AprobarPedidoMaterialCommand;

public record AprobarPedidoMaterialCommand : IRequest<PedidoMaterialDto>
{
    public int PedidoMaterialId { get; init; }
}

public class AprobarPedidoMaterialCommandHandler : IRequestHandler<AprobarPedidoMaterialCommand, PedidoMaterialDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AprobarPedidoMaterialCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PedidoMaterialDto> Handle(AprobarPedidoMaterialCommand request, CancellationToken cancellationToken)
    {
        var pedido = await _context.PedidoMateriales
            .Include(p => p.EstadoPedidoMaterial)
            .Include(p => p.Material)
            .ThenInclude(m => m.UnidadMedida)
            .Include(p => p.BodegaSolicita)
            .ThenInclude(b => b.Ubicacion)
            .ThenInclude(u => u.Municipio)
            .ThenInclude(m => m.Departamento)
            .ThenInclude(d => d.Pais)
            .FirstOrDefaultAsync(p => p.Id == request.PedidoMaterialId && p.Status == "A", cancellationToken);

        if (pedido == null)
        {
            throw new NotFoundException(nameof(PedidoMaterial), request.PedidoMaterialId);
        }

        if (pedido.EstadoPedidoMaterialId != EstadosPedidoMaterialConstants.PENDIENTE.Id)
        {
            throw new CustomValidationException($"No se puede aprobar un pedido en estado {pedido.EstadoPedidoMaterial.Descripcion}.");
        }

        var pesoTotalPedido =pedido.Cantidad * pedido.Material.Peso;

        var bodegaExistencias = await _context.InventarioBodegas
            .Include(v => v.Bodega)
            .Include(v => v.Material)
            .Where(v => v.Status == "A" && v.BodegaId != pedido.BodegaSolicitaId
                && v.MaterialId == pedido.MaterialId && v.CantidadDisponible >= pedido.Cantidad)
            .OrderByDescending(v => v.CantidadDisponible)
            .FirstOrDefaultAsync(cancellationToken);

        if (bodegaExistencias == null)
        {
            throw new CustomValidationException("No hay existencias del material solicitado.");
        }

        var dateProgramado = DateTime.Now.Date.AddDays(1);

        var pedidosVigentes = _context.MovimientoBodegas
            .Include(m => m.EstadoMovimientoBodega)
            .Include(m => m.Vehiculo)
            .Include(m => m.Conductor)
            .Where(m => m.Status == "A"
                && ESTADOS_PEDIDOS_VIGENTES.Contains(m.EstadoMovimientoBodegaId)
                && m.FechaInicioProgramado.Value.Date == dateProgramado);

        var vehiculosVigentes = await pedidosVigentes
            .Select(p => p.VehiculoId)
            .ToListAsync(cancellationToken);

        var conductoresVigentes = await pedidosVigentes
            .Select(p => p.ConductorId)
            .ToListAsync(cancellationToken);
        
        var pesoFaltanteAsignar = (double)pesoTotalPedido;

        do
        {
            var candidato = await _context.VehiculoConductores
                .Include(vc => vc.Conductor)
                .Include(vc => vc.Vehiculo)
                .Where(vc => vc.Status == "A" && vc.Conductor.Status == "A" && vc.Vehiculo.Status == "A"
                    && !vehiculosVigentes.Contains(vc.VehiculoId) && !conductoresVigentes.Contains(vc.ConductorId))
                .OrderBy(vc => Math.Abs((double)(vc.Vehiculo.CapacidadCarga - pesoFaltanteAsignar)))
                .FirstOrDefaultAsync(cancellationToken);

            if (candidato == null)
            {
                throw new CustomValidationException("No existen vehiculos/conductores disponibles para entregar el pedido.");
            }

            double cantidadAsignar = 0;
            if (candidato.Vehiculo.CapacidadCarga >= pesoFaltanteAsignar)
            {
                cantidadAsignar = pesoFaltanteAsignar;
            }
            else
            {
                cantidadAsignar = (double)candidato.Vehiculo.CapacidadCarga;
            }

            var movimiento = new MovimientoBodega
            {
                EstadoMovimientoBodegaId = EstadosMovimientoBodegaConstants.PROGRAMADO.Id,
                PedidoMaterial = pedido,
                BodegaOrigenId = bodegaExistencias.BodegaId,
                BodegaDestinoId = pedido.BodegaSolicitaId,
                FechaInicioProgramado = dateProgramado,
                MaterialId = pedido.MaterialId,
                Cantidad = cantidadAsignar,
                VehiculoId = candidato.VehiculoId,
                ConductorId = candidato.ConductorId,
            };

            await _context.MovimientoBodegas.AddAsync(movimiento, cancellationToken);
            await _context.BitacoraEstadoMovimientoBodegas.AddAsync(new BitacoraEstadoMovimientoBodega
            {
                EstadoMovimientoBodegaId = EstadosMovimientoBodegaConstants.PENDIENTE.Id,
                MovimientoBodega = movimiento
            }, cancellationToken);
            await _context.BitacoraEstadoMovimientoBodegas.AddAsync(new BitacoraEstadoMovimientoBodega
            {
                EstadoMovimientoBodegaId = EstadosMovimientoBodegaConstants.PROGRAMADO.Id,
                MovimientoBodega = movimiento
            }, cancellationToken);

            conductoresVigentes.Add(candidato.ConductorId);
            vehiculosVigentes.Add(candidato.VehiculoId);

            pesoFaltanteAsignar = pesoFaltanteAsignar - cantidadAsignar;

        }
        while (pesoFaltanteAsignar > 0);

       
        bodegaExistencias.CantidadDisponible = bodegaExistencias.CantidadDisponible - pedido.Cantidad;
        bodegaExistencias.CantidadReservada = bodegaExistencias.CantidadReservada + pedido.Cantidad;

        pedido.EstadoPedidoMaterialId = EstadosPedidoMaterialConstants.APROBADO.Id;
        await _context.BitacoraEstadoPedidoMateriales.AddAsync(new BitacoraEstadoPedidoMaterial
        {
            EstadoPedidoMaterialId = EstadosPedidoMaterialConstants.APROBADO.Id,
            PedidoMaterial = pedido
        }, cancellationToken);

        pedido.EstadoPedidoMaterialId = EstadosPedidoMaterialConstants.PROGRAMADO.Id;
        await _context.BitacoraEstadoPedidoMateriales.AddAsync(new BitacoraEstadoPedidoMaterial
        {
            EstadoPedidoMaterialId = EstadosPedidoMaterialConstants.PROGRAMADO.Id,
            PedidoMaterial = pedido
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        pedido = await _context.PedidoMateriales
            .Include(p => p.EstadoPedidoMaterial)
            .Include(p => p.Material)
            .ThenInclude(m => m.UnidadMedida)
            .Include(p => p.BodegaSolicita)
            .ThenInclude(b => b.Ubicacion)
            .ThenInclude(u => u.Municipio)
            .ThenInclude(m => m.Departamento)
            .ThenInclude(d => d.Pais)
            .FirstOrDefaultAsync(p => p.Id == request.PedidoMaterialId && p.Status == "A", cancellationToken);
        return _mapper.Map<PedidoMaterial, PedidoMaterialDto>(pedido);
    }

    private static readonly int?[] ESTADOS_PEDIDOS_VIGENTES = new int?[] { EstadosMovimientoBodegaConstants.PENDIENTE.Id, EstadosMovimientoBodegaConstants.PROGRAMADO.Id, EstadosMovimientoBodegaConstants.CARGADO.Id };

}
