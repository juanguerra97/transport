using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;

namespace seminario.Application.TipoPlantas.Queries.GetTipoPlantas;
public class GetTipoPlantasQuery : IRequest<List<TipoPlantaDto>>
{
}

public class GetTipoPlantasQueryHandler : IRequestHandler<GetTipoPlantasQuery, List<TipoPlantaDto>>
{

    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTipoPlantasQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TipoPlantaDto>> Handle(GetTipoPlantasQuery request, CancellationToken cancellationToken)
    {
        return await _context.TipoPlanta
            .Where(t => t.Status == "A")
            .OrderBy(t => t.Id)
            .ProjectTo<TipoPlantaDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
