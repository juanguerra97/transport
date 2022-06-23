using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;

namespace seminario.Application.Departamentos.Queries.GetDepartamentos;
public record GetDepartamentosQuery : IRequest<List<DepartamentoDto>>
{
    public int? PaisId { get; init; }
}

public class GetDepartamentosQueryHandler : IRequestHandler<GetDepartamentosQuery, List<DepartamentoDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDepartamentosQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DepartamentoDto>> Handle(GetDepartamentosQuery request, CancellationToken cancellationToken)
    {
        return await _context.Departamento
            .Where(d => d.Status == "A" && (request.PaisId == null || d.PaisId == request.PaisId))
            .OrderBy(d => d.PaisId)
            .ThenBy(d => d.Descripcion)
            .ProjectTo<DepartamentoDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}