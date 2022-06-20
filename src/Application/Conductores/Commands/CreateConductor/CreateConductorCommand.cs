using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.Conductores.Commands.CreateConductor;

public record CreateConductorCommand : IRequest<int?>
{
    public string UserId { get; init; }
    public string? NoLicencia { get; init; }
}

public class CreateConductorCommandHandler : IRequestHandler<CreateConductorCommand, int?>
{
    private readonly IApplicationDbContext _context;

    public CreateConductorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int?> Handle(CreateConductorCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.ApplicationUsers
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(ApplicationUser), request.UserId);
        }

        if (true == (await _context.Conductores.AnyAsync(c => c.UserId == request.UserId && c.Status != "X", cancellationToken)))
        {
            throw new CustomValidationException($"El usuario {user.FirstName} {user.LastName}({user.UserName}) ya se registro como conductor.");
        }

        var entity = new Conductor
        {
            User = user,
            NoLicencia = request.NoLicencia
        };

        await _context.Conductores.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
