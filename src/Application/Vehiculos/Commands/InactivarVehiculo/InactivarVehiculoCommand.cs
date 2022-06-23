using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.Vehiculos.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Vehiculos.Commands.InactivarVehiculo;

public record InactivarVehiculoCommand : IRequest<VehiculoDto>
{
    public int VehiculoId { get; init; }
}

public class InactivarVehiculoCommandHandler : IRequestHandler<InactivarVehiculoCommand, VehiculoDto>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;

    public InactivarVehiculoCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<VehiculoDto> Handle(InactivarVehiculoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Vehiculo
            .FirstOrDefaultAsync(v => v.Id == request.VehiculoId && v.Status == "A", cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Vehiculo), request.VehiculoId);
        }

        entity.Status = "I";

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Vehiculo, VehiculoDto>(entity);
    }
}
