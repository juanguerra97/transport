using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;

namespace seminario.Application.Materiales.Queries.SearchMaterialByDescripcion;
public record SearchMaterialByDescripcionQuery : IRequest<List<MaterialDto>>
{
    public string Descripcion { get; set; }
    public int MaxResults { get; init; }
}

public class SearchMaterialByDescripcionQueryHandler : IRequestHandler<SearchMaterialByDescripcionQuery, List<MaterialDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public SearchMaterialByDescripcionQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<MaterialDto>> Handle(SearchMaterialByDescripcionQuery request, CancellationToken cancellationToken)
    {
        var descripcionLike = "%" + request.Descripcion.Replace(" ", "%").ToUpper() + "%";
        return await _context.Material
            .Where(m => m.Status == "A" && EF.Functions.Like(m.Descripcion.ToUpper(), descripcionLike))
            .Take(request.MaxResults)
            .ProjectTo<MaterialDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
