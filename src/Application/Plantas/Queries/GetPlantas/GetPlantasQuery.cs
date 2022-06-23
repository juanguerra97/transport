
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;

namespace seminario.Application.Plantas.Queries.GetPlantas;
public class GetPlantasQuery : IRequest<List<PlantaDto>>
{
}

public class GetPlantasQueryHandler : IRequestHandler<GetPlantasQuery, List<PlantaDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPlantasQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<PlantaDto>> Handle(GetPlantasQuery request, CancellationToken cancellationToken)
    {
        return await _context.Planta
            .Where(p => p.Status == "A")
            .OrderBy(p => p.Descripcion).ThenBy(p => p.Id)
            .ProjectTo<PlantaDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
