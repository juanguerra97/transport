using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.Conductores.Queries.GetConductorById;

public record GetConductorByIdQuery : IRequest<ConductorDto>
{
    public int ConductorId { get; init; }
}

public class GetConductorByIdQueryHandler : IRequestHandler<GetConductorByIdQuery, ConductorDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetConductorByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ConductorDto> Handle(GetConductorByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Conductor
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == request.ConductorId && c.Status != "X", cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Conductor), request.ConductorId);
        }

        return _mapper.Map<Conductor, ConductorDto>(entity);
    }
}
