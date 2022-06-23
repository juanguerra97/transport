using MediatR;
using Microsoft.AspNetCore.Identity;
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
    private readonly UserManager<ApplicationUser> _userManager;

    public CreateConductorCommandHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<int?> Handle(CreateConductorCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.ApplicationUser
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(ApplicationUser), request.UserId);
        }

        if (true == (await _context.Conductor.AnyAsync(c => c.UserId == request.UserId && c.Status != "X", cancellationToken)))
        {
            throw new CustomValidationException($"El usuario {user.FirstName} {user.LastName}({user.UserName}) ya se registro como conductor.");
        }

        if (!(await _userManager.IsInRoleAsync(user, "Conductor")))
        {
            await _userManager.AddToRoleAsync(user, "Conductor");
        }

        var entity = new Conductor
        {
            User = user,
            NoLicencia = request.NoLicencia
        };

        await _context.Conductor.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
