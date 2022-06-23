using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;

namespace seminario.Application.TipoMateriales.Queries.GetTipoMateriales;
public class GetTipoMaterialesQuery : IRequest<List<TipoMaterialDto>>
{
}

public class GetTipoMaterialesQueryHandler : IRequestHandler<GetTipoMaterialesQuery, List<TipoMaterialDto>>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;

    public GetTipoMaterialesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TipoMaterialDto>> Handle(GetTipoMaterialesQuery request, CancellationToken cancellationToken)
    {
        return await _context.TipoMaterial
            .Where(tm => tm.Status == "A")
            .OrderBy(tm => tm.Id)
            .ProjectTo<TipoMaterialDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
