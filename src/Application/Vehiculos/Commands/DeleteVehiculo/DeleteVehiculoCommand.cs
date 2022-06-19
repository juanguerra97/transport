using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.Vehiculos.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Vehiculos.Commands.DeleteVehiculo;

public record DeleteVehiculoCommand : IRequest<VehiculoDto>
{
    public int VehiculoId { get; init; }
}

public class DeleteVehiculoCommandHandler : IRequestHandler<DeleteVehiculoCommand, VehiculoDto>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;

    public DeleteVehiculoCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<VehiculoDto> Handle(DeleteVehiculoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Vehiculos
            .FirstOrDefaultAsync(v => v.Id == request.VehiculoId && v.Status != "X", cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Vehiculo), request.VehiculoId);
        }

        entity.Status = "X";
        foreach (var vc in _context.VehiculoConductores.Where(vc => vc.VehiculoId == request.VehiculoId))
        {
            vc.Status = "X";
        }
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Vehiculo, VehiculoDto>(entity);
    }
}