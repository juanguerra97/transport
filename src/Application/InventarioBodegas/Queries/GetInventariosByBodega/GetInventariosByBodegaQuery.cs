using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Interfaces;
using seminario.Application.Common.Models;

namespace seminario.Application.InventarioBodegas.Queries.GetInventariosByBodega;
public record GetInventariosByBodegaQuery : IRequest<PaginatedList<InventarioBodegaDto>>
{
    public int BodegaId { get; set; }
    public string? DescripcionMaterial { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetInventariosByBodegaQueryHandler : IRequestHandler<GetInventariosByBodegaQuery, PaginatedList<InventarioBodegaDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetInventariosByBodegaQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<InventarioBodegaDto>> Handle(GetInventariosByBodegaQuery request, CancellationToken cancellationToken)
    {
        var descripcionLike = "%" + request.DescripcionMaterial?.Replace(" ", "%")?.ToUpper() + "%";
        return await PaginatedList<InventarioBodegaDto>.CreateAsync(
            _context.InventarioBodegas
            .Where(ib => ib.BodegaId == request.BodegaId 
                && (request.DescripcionMaterial == null 
                    || EF.Functions.Like(ib.Material.Descripcion.ToUpper(), descripcionLike)))
            .OrderBy(ib => ib.MaterialId)
            .ProjectTo<InventarioBodegaDto>(_mapper.ConfigurationProvider)
            , request.PageNumber, request.PageSize);
    }
}