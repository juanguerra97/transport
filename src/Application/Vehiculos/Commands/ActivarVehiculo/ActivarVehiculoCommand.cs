using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.Vehiculos.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Vehiculos.Commands.ActivarVehiculo;
public record ActivarVehiculoCommand : IRequest<VehiculoDto>
{
    public int VehiculoId { get; set; }
}

public class ActivarVehiculoCommandHandler : IRequestHandler<ActivarVehiculoCommand, VehiculoDto>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;

    public ActivarVehiculoCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<VehiculoDto> Handle(ActivarVehiculoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Vehiculos
            .FirstOrDefaultAsync(v => v.Id == request.VehiculoId && v.Status == "I", cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Vehiculo), request.VehiculoId);
        }

        entity.Status = "A";

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Vehiculo, VehiculoDto>(entity);
    }
}
