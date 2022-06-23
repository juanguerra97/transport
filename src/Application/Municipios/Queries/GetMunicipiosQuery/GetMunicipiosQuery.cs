using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;

namespace seminario.Application.Municipios.Queries.GetMunicipiosQuery;
public record GetMunicipiosQuery : IRequest<List<MunicipioDto>>
{
    public int? PaisId { get; init; }
    public int? DepartamentoId { get; init; }
}

public class GetMunicipiosQueryHandler : IRequestHandler<GetMunicipiosQuery, List<MunicipioDto>>
{

    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMunicipiosQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<MunicipioDto>> Handle(GetMunicipiosQuery request, CancellationToken cancellationToken)
    {
        return await _context.Municipio
            .Where(m => m.Status == "A" 
                && (request.PaisId == null || m.Departamento.PaisId == request.PaisId)
                && (request.DepartamentoId == null || m.DepartamentoId == request.DepartamentoId))
            .OrderBy(m => m.Departamento.PaisId)
            .ThenBy(m => m.DepartamentoId)
            .ThenBy(m => m.Descripcion)
            .ProjectTo<MunicipioDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}