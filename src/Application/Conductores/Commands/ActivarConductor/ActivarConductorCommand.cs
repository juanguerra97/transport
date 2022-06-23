using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.Conductores.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Conductores.Commands.ActivarConductor;

public record ActivarConductorCommand : IRequest<ConductorDto>
{
    public int ConductorId { get; init; }
}

public class ActivarConductorCommandHandler : IRequestHandler<ActivarConductorCommand, ConductorDto>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;

    public ActivarConductorCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ConductorDto> Handle(ActivarConductorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Conductor
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == request.ConductorId && c.Status == "I", cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Conductor), request.ConductorId);
        }

        entity.Status = "A";

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Conductor, ConductorDto>(entity);
    }
}