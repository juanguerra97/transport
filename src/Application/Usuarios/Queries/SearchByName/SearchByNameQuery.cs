
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;

namespace seminario.Application.Usuarios.Queries.SearchByName;
public record SearchByNameQuery : IRequest<List<UsuarioDto>>
{
    public string Name { get; init; }
    public int MaxResults { get; init; }
}


public class SearchByNameQueryHandler : IRequestHandler<SearchByNameQuery, List<UsuarioDto>>
{

    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public SearchByNameQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UsuarioDto>> Handle(SearchByNameQuery request, CancellationToken cancellationToken)
    {
        string name = "%" + request.Name?.Replace(" ", "%").ToUpper() + "%";

        return await _context.ApplicationUsers
            .Where(u => EF.Functions.Like(u.FirstName.ToUpper() + " " + u.LastName.ToUpper(), name))
            .Take(request.MaxResults)
            .ProjectTo<UsuarioDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
