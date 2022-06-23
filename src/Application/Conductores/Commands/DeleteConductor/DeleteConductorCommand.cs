using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.Conductores.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Conductores.Commands.DeleteConductor;

public record DeleteConductorCommand : IRequest<ConductorDto>
{
    public int ConductorId { get; init; }
}

public class DeleteConductorCommandHandler : IRequestHandler<DeleteConductorCommand, ConductorDto>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public DeleteConductorCommandHandler(IApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<ConductorDto> Handle(DeleteConductorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Conductor
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == request.ConductorId && c.Status != "X", cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Conductor), request.ConductorId);
        }

        if ((await _userManager.IsInRoleAsync(entity.User, "Conductor")))
        {
            await _userManager.RemoveFromRoleAsync(entity.User, "Conductor");
        }

        entity.Status = "X";
        foreach(var vc in _context.VehiculoConductor.Where(vc => vc.ConductorId == request.ConductorId && vc.Status != "X"))
        {
            vc.Status = "X";
        }

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Conductor, ConductorDto>(entity);
    }
}
