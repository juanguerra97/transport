using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.VehiculoConductores.Commands.DeleteVehiculoConductor;

public record DeleteVehiculoConductorCommand : IRequest
{
    public int VehiculoId { get; init; }
    public int ConductorId { get; init; }
}

public class DeleteVehiculoConductorCommandHandler : IRequestHandler<DeleteVehiculoConductorCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteVehiculoConductorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteVehiculoConductorCommand request, CancellationToken cancellationToken)
    {

        var entity = await _context.VehiculoConductores
            .FirstOrDefaultAsync(vc => vc.VehiculoId == request.VehiculoId && vc.ConductorId == vc.ConductorId && vc.Status == "A", cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(VehiculoConductor), $"({request.VehiculoId},{request.ConductorId})");
        }

        entity.Status = "X";

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}