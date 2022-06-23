using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;

namespace seminario.Application.Proveedores.Queries.SearchProveedorByName;
public class SearchProveedorByNameQuery : IRequest<List<ProveedorDto>>
{
    public string Name { get; init; }
    public int MaxResults { get; init; }
}

public class SearchProveedorByNameQueryHandler : IRequestHandler<SearchProveedorByNameQuery, List<ProveedorDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public SearchProveedorByNameQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProveedorDto>> Handle(SearchProveedorByNameQuery request, CancellationToken cancellationToken)
    {
        string nameLike = "%" + request.Name?.Replace(" ", "%").ToUpper() + "%";
        return await _context.ProveedorMaterial
            .Where(p => EF.Functions.Like(p.Nombre, nameLike))
            .Take(request.MaxResults)
            .ProjectTo<ProveedorDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
