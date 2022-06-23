using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Constants;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.MovimientosBodega.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.MovimientosBodega.Commands.EntregarMovimientoBodega;

public record EntregarMovimientoBodegaCommand : IRequest<MovimientoBodegaDto>
{
    public int MovimientoBodegaId { get; init; }
}

public class EntregarMovimientoBodegaCommandHandler : IRequestHandler<EntregarMovimientoBodegaCommand, MovimientoBodegaDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EntregarMovimientoBodegaCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MovimientoBodegaDto> Handle(EntregarMovimientoBodegaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.MovimientoBodegas
            .Include(m => m.EstadoMovimientoBodega)
            .Include(m => m.PedidoMaterial.Material.UnidadMedida)
            .FirstOrDefaultAsync(m => m.Id == request.MovimientoBodegaId && m.Status != "X", cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(MovimientoBodega), request.MovimientoBodegaId);
        }

        if (entity.EstadoMovimientoBodegaId != EstadosMovimientoBodegaConstants.CARGADO.Id)
        {
            throw new CustomValidationException($"Un movimiento en estado {entity.EstadoMovimientoBodega.Descripcion} no puede ser entregado.");
        }

        var inventarioOrigen = await _context.InventarioBodegas
            .FirstOrDefaultAsync(iv => iv.BodegaId == entity.BodegaOrigenId && iv.MaterialId == entity.PedidoMaterial.MaterialId && iv.Status != "X", cancellationToken);

        if (inventarioOrigen == null)
        {
            throw new CustomValidationException("No existe un inventario de origen.");
        }

        if (inventarioOrigen.CantidadReservada < entity.Cantidad)
        {
            throw new CustomValidationException("La cantidad reservada del inventario de origen no es suficiente para completar la entrega.");
        }

        var inventario = await _context.InventarioBodegas
            .FirstOrDefaultAsync(iv => iv.BodegaId == entity.BodegaDestinoId && iv.MaterialId == entity.PedidoMaterial.MaterialId && iv.Status != "X", cancellationToken);

        if (inventario == null)
        {
            inventario = new InventarioBodega
            {
                BodegaId = entity.BodegaDestinoId,
                MaterialId = entity.PedidoMaterial.MaterialId,
                CantidadDisponible = entity.Cantidad,
                CantidadReservada = 0
            };
            await _context.InventarioBodegas.AddAsync(inventario, cancellationToken);
        } else
        {
            inventario.CantidadDisponible = inventario.CantidadDisponible + entity.Cantidad;
        }

        inventarioOrigen.CantidadReservada = inventarioOrigen.CantidadReservada - entity.Cantidad;

        entity.EstadoMovimientoBodegaId = EstadosMovimientoBodegaConstants.ENTREGADO.Id;

        await _context.BitacoraEstadoMovimientoBodegas.AddAsync(new BitacoraEstadoMovimientoBodega
        {
            MovimientoBodegaId = entity.Id,
            EstadoMovimientoBodegaId = EstadosMovimientoBodegaConstants.ENTREGADO.Id
        }, cancellationToken);

        if(true == (await _context.MovimientoBodegas
            .Where(m => m.Status != "X" && m.PedidoMaterialId == entity.PedidoMaterialId && m.Id != entity.Id && m.EstadoMovimientoBodegaId != EstadosMovimientoBodegaConstants.ANULADO.Id)
            .AllAsync(m => m.EstadoMovimientoBodegaId == EstadosMovimientoBodegaConstants.ENTREGADO.Id, cancellationToken)))
        {
            var pedido = entity.PedidoMaterial;
            pedido.EstadoPedidoMaterialId = EstadosPedidoMaterialConstants.COMPLETADO.Id;
            await _context.BitacoraEstadoPedidoMateriales.AddAsync(new BitacoraEstadoPedidoMaterial
            {
                PedidoMaterialId = pedido.Id,
                EstadoPedidoMaterialId = EstadosPedidoMaterialConstants.COMPLETADO.Id
            });
        }

        await _context.SaveChangesAsync(cancellationToken);

        entity = await _context.MovimientoBodegas
            .Include(m => m.EstadoMovimientoBodega)
            .Include(m => m.PedidoMaterial.Material.UnidadMedida)
            .Include(m => m.BodegaOrigen.Ubicacion.Municipio.Departamento.Pais)
            .Include(m => m.BodegaDestino.Ubicacion.Municipio.Departamento.Pais)
            .Include(m => m.Vehiculo)
            .Include(m => m.Conductor.User)
            .FirstOrDefaultAsync(m => m.Id == request.MovimientoBodegaId && m.Status != "X", cancellationToken);

        return _mapper.Map<MovimientoBodega, MovimientoBodegaDto>(entity);
    }
}