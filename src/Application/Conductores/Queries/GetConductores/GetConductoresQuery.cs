using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;
using seminario.Application.Common.Models;

namespace seminario.Application.Conductores.Queries.GetConductores;
public record GetConductoresQuery : IRequest<PaginatedList<ConductorDto>>
{
    public string? Nombre { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetConductoresQueryHandler : IRequestHandler<GetConductoresQuery, PaginatedList<ConductorDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetConductoresQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ConductorDto>> Handle(GetConductoresQuery request, CancellationToken cancellationToken)
    {
        var nombreLike = "%" + request.Nombre?.Replace(" ", "%")?.ToUpper() + "%";
        return await PaginatedList<ConductorDto>.CreateAsync(
            _context.Conductores
            .Include(c => c.User)
            .Where(c => c.Status != "X" 
                && (request.Nombre == null || EF.Functions.Like((c.User.FirstName + " " + c.User.LastName).ToUpper(), nombreLike)))
            .OrderBy(c => c.Id)
            .ProjectTo<ConductorDto>(_mapper.ConfigurationProvider)
            , request.PageNumber, request.PageSize);
    }
}
