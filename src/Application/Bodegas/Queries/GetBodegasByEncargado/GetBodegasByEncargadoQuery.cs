using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using seminario.Application.Common.Interfaces;
using seminario.Application.Common.Models;

namespace seminario.Application.Bodegas.Queries.GetBodegasByEncargado;
public record GetBodegasByEncargadoQuery : IRequest<PaginatedList<BodegaDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string EncargadoId { get; set; }
}

public record GetBodegasByEncargadoQueryHandler : IRequestHandler<GetBodegasByEncargadoQuery, PaginatedList<BodegaDto>>
{

    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBodegasByEncargadoQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BodegaDto>> Handle(GetBodegasByEncargadoQuery request, CancellationToken cancellationToken)
    {
        return await PaginatedList<BodegaDto>.CreateAsync(
            _context.AdminBodega
            .Where(ad => ad.Status == "A" && ad.UserId == request.EncargadoId)
            .Select(ad => ad.Bodega)
            .OrderBy(b => b.Id)
            .ProjectTo<BodegaDto>(_mapper.ConfigurationProvider)
            , request.PageNumber, request.PageSize);
    }
}
