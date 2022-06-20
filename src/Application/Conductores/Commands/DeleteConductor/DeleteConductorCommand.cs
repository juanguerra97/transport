using AutoMapper;
using MediatR;
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

    public DeleteConductorCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ConductorDto> Handle(DeleteConductorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Conductores
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == request.ConductorId && c.Status != "X", cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Conductor), request.ConductorId);
        }

        entity.Status = "X";
        foreach(var vc in _context.VehiculoConductores.Where(vc => vc.ConductorId == request.ConductorId && vc.Status != "X"))
        {
            vc.Status = "X";
        }

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Conductor, ConductorDto>(entity);
    }
}
