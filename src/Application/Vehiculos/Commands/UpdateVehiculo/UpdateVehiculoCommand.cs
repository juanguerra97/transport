using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.Vehiculos.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Vehiculos.Commands.UpdateVehiculo;
public record UpdateVehiculoCommand : IRequest<VehiculoDto>
{
    public int VehiculoId { get; set; }
    public bool? EsUsoInterno { get; init; }
    public string? Codigo { get; init; }
    public string? Placa { get; init; }
    public string? Descripcion { get; init; }
    public string? Detalle { get; init; }
    public double? CapacidadCarga { get; init; }
}

public class UpdateVehiculoCommandHandler : IRequestHandler<UpdateVehiculoCommand, VehiculoDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateVehiculoCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<VehiculoDto> Handle(UpdateVehiculoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Vehiculo
            .FirstOrDefaultAsync(v => v.Id == request.VehiculoId && v.Status != "X", cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Vehiculo), request.VehiculoId);
        }
        entity.EsUsoInterno = request.EsUsoInterno;
        entity.Codigo = request.Codigo;
        entity.Placa = request.Placa;
        entity.Descripcion = request.Descripcion;
        entity.Detalle = request.Detalle;
        entity.CapacidadCarga = request.CapacidadCarga;

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Vehiculo, VehiculoDto>(entity);
    }
}
