using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Constants;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.MovimientosBodega.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.MovimientosBodega.Commands;

public record CargarMovimientoBodegaCommand : IRequest<MovimientoBodegaDto>
{
    public int MovimientoBodegaId { get; init; }
}

public class CargarMovimientoBodegaCommandHandler : IRequestHandler<CargarMovimientoBodegaCommand, MovimientoBodegaDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CargarMovimientoBodegaCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MovimientoBodegaDto> Handle(CargarMovimientoBodegaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.MovimientoBodegas
            .Include(m => m.EstadoMovimientoBodega)
            .Include(m => m.PedidoMaterial.Material.UnidadMedida)
            .FirstOrDefaultAsync(m => m.Id == request.MovimientoBodegaId && m.Status != "X", cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(MovimientoBodega), request.MovimientoBodegaId);
        }

        if (entity.EstadoMovimientoBodegaId != EstadosMovimientoBodegaConstants.PROGRAMADO.Id)
        {
            throw new CustomValidationException($"Un movimiento en estado {entity.EstadoMovimientoBodega.Descripcion} no puede ser cargado.");
        }

        entity.EstadoMovimientoBodegaId = EstadosMovimientoBodegaConstants.CARGADO.Id;

        await _context.BitacoraEstadoMovimientoBodegas.AddAsync(new BitacoraEstadoMovimientoBodega
        {
            MovimientoBodegaId = entity.Id,
            EstadoMovimientoBodegaId = EstadosMovimientoBodegaConstants.CARGADO.Id
        }, cancellationToken);
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
