using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;

namespace seminario.Application.MovimientosBodega.Queries.GetMovimientosBodegaByPedido;

public record GetMovimientosBodegaByPedidoQuery : IRequest<List<MovimientoBodegaDto>>
{
    public int PedidoMaterialId { get; init; }
}

public class GetMovimientosBodegaByPedidoQueryHandler : IRequestHandler<GetMovimientosBodegaByPedidoQuery, List<MovimientoBodegaDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMovimientosBodegaByPedidoQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<MovimientoBodegaDto>> Handle(GetMovimientosBodegaByPedidoQuery request, CancellationToken cancellationToken)
    {
        return await _context.MovimientoBodega
            .Where(m => m.PedidoMaterialId == request.PedidoMaterialId && m.Status == "A")
            .OrderBy(m => m.Id)
            .ProjectTo<MovimientoBodegaDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
