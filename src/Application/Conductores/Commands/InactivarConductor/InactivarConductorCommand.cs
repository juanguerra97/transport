using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.Conductores.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Conductores.Commands.InactivarConductor;

public record InactivarConductorCommand : IRequest<ConductorDto>
{
    public int ConductorId { get; init; }
}

public class InactivarConductorCommandHandler : IRequestHandler<InactivarConductorCommand, ConductorDto>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;

    public InactivarConductorCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ConductorDto> Handle(InactivarConductorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Conductores
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == request.ConductorId && c.Status == "A", cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Conductor), request.ConductorId);
        }

        entity.Status = "I";

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Conductor, ConductorDto>(entity);
    }
}
