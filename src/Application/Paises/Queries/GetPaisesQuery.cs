using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;

namespace seminario.Application.Paises.Queries;
public record GetPaisesQuery : IRequest<List<PaisDto>>
{
}

public class GetPaisesQueryHandler : IRequestHandler<GetPaisesQuery, List<PaisDto>>
{

    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPaisesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PaisDto>> Handle(GetPaisesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Paises
            .Where(p => p.Status == "A")
            .OrderBy(p => p.Id)
            .ProjectTo<PaisDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}