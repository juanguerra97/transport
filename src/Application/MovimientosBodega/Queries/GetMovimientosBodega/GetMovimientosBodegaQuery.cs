using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Constants;
using seminario.Application.Common.Interfaces;
using seminario.Application.Common.Models;

namespace seminario.Application.MovimientosBodega.Queries.GetMovimientosBodega;

public record GetMovimientosBodegaQuery : IRequest<PaginatedList<MovimientoBodegaDto>>
{
    public int? BodegaOrigenId { get; init; }
    public int? BodegaDestinoId { get; init; }
    public string? DescripcionMaterial { get; init; }
    public int? ConductorId { get; init; }
    public int? VehiculoId { get; init; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetMovimientosBodegaQueryHandler : IRequestHandler<GetMovimientosBodegaQuery, PaginatedList<MovimientoBodegaDto>>
{

    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMovimientosBodegaQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<MovimientoBodegaDto>> Handle(GetMovimientosBodegaQuery request, CancellationToken cancellationToken)
    {
        var descripcionLike = "%" + request.DescripcionMaterial?.Replace(" ", "%")?.ToUpper() + "%";

        return await PaginatedList<MovimientoBodegaDto>.CreateAsync(
            _context.MovimientoBodegas
            .Where(m => m.Status != "X"
                && (ESTADOS.Contains(m.EstadoMovimientoBodegaId))
                && (request.DescripcionMaterial == null || EF.Functions.Like((m.PedidoMaterial.Material.Descripcion + " " + m.PedidoMaterial.Material.Detalle).ToUpper(), descripcionLike))
                && (request.BodegaOrigenId == null || m.BodegaOrigenId == request.BodegaOrigenId)
                && (request.BodegaDestinoId == null || m.BodegaDestinoId == request.BodegaDestinoId)
                && (request.ConductorId == null || m.ConductorId == request.ConductorId)
                && (request.VehiculoId == null || m.VehiculoId == request.VehiculoId))
            .OrderByDescending(m => m.Id)
            .ProjectTo<MovimientoBodegaDto>(_mapper.ConfigurationProvider)
            , request.PageNumber, request.PageSize);
    }

    private static readonly int?[] ESTADOS = new int?[] { EstadosMovimientoBodegaConstants.PROGRAMADO.Id, EstadosMovimientoBodegaConstants.CARGADO.Id, EstadosMovimientoBodegaConstants.ENTREGADO.Id };
}