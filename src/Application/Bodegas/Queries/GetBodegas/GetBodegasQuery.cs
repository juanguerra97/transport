using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Enums;

namespace seminario.Application.Bodegas.Queries.GetBodegas;
public record GetBodegasQuery : IRequest<List<BodegaDto>>
{
}

public class GetBodegasQueryHandler : IRequestHandler<GetBodegasQuery, List<BodegaDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBodegasQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<BodegaDto>> Handle(GetBodegasQuery request, CancellationToken cancellationToken)
    {
        return await _context.Bodegas
            .Where(p => p.Status == "A" && p.TipoBodega == TipoBodega.BODEGA)
            .OrderBy(p => p.Descripcion).ThenBy(p => p.Id)
            .ProjectTo<BodegaDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
