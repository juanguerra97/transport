using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.VehiculoConductores.Commands.CreateVehiculoConductor;

public record CreateVehiculoConductorCommand : IRequest
{
    public int VehiculoId { get; init; }
    public int ConductorId { get; init; }
}

public class CreateVehiculoConductorCommandHandler : IRequestHandler<CreateVehiculoConductorCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateVehiculoConductorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CreateVehiculoConductorCommand request, CancellationToken cancellationToken)
    {
        var vehiculo = await _context.Vehiculos
            .FirstOrDefaultAsync(v => v.Id == request.VehiculoId && v.Status != "X");
        if (vehiculo == null)
        {
            throw new NotFoundException(nameof(Vehiculo), request.VehiculoId);
        }

        var conductor = await _context.Conductores
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == request.ConductorId && c.Status != "X");
        if (conductor == null)
        {
            throw new NotFoundException(nameof(Conductor), request.ConductorId);
        }

        VehiculoConductor? entity = await _context.VehiculoConductores
            .Include(vc => vc.Conductor)
            .Include(vc => vc.Vehiculo)
            .FirstOrDefaultAsync(vc => vc.VehiculoId == request.VehiculoId && vc.ConductorId == request.ConductorId, cancellationToken);
        
        if (entity != null)
        {
            if (entity.Status == "X")
            {
                entity.Status = "A";
            } else
            {
                throw new CustomValidationException("La relacion Vehiculo-Conductor ya existe.");
            }
        } else
        {
            entity = new VehiculoConductor
            {
                Vehiculo = vehiculo,
                Conductor = conductor
            };
            await _context.VehiculoConductores.AddAsync(entity);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}