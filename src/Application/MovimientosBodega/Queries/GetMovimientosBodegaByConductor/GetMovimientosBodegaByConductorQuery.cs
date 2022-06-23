using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Constants;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.Common.Models;

namespace seminario.Application.MovimientosBodega.Queries.GetMovimientosBodegaByConductor;

public record GetMovimientosBodegaByConductorQuery : IRequest<PaginatedList<MovimientoBodegaDto>>
{
    public int? BodegaOrigenId { get; init; }
    public int? BodegaDestinoId { get; init; }
    public string? DescripcionMaterial { get; init; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetMovimientosBodegaByConductorQueryHandler : IRequestHandler<GetMovimientosBodegaByConductorQuery, PaginatedList<MovimientoBodegaDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetMovimientosBodegaByConductorQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<PaginatedList<MovimientoBodegaDto>> Handle(GetMovimientosBodegaByConductorQuery request, CancellationToken cancellationToken)
    {

        var userId = _currentUserService.UserId;
        var conductor = await _context.Conductores
            .FirstOrDefaultAsync(c => c.UserId == userId && c.Status != "X", cancellationToken);

        if (conductor == null)
        {
            throw new CustomValidationException("El usuario logueado no esta registrado como conductor.");
        }

        var descripcionLike = "%" + request.DescripcionMaterial?.Replace(" ", "%")?.ToUpper() + "%";
        var todayDate = DateTime.Now.Date;
        return await PaginatedList<MovimientoBodegaDto>.CreateAsync(
            _context.MovimientoBodegas
            .Where(m => m.Status != "X" && m.ConductorId == conductor.Id
                && (ESTADOS.Contains(m.EstadoMovimientoBodegaId))
                && (request.DescripcionMaterial == null || EF.Functions.Like((m.PedidoMaterial.Material.Descripcion + " " + m.PedidoMaterial.Material.Detalle).ToUpper(), descripcionLike))
                && (request.BodegaOrigenId == null || m.BodegaOrigenId == request.BodegaOrigenId)
                && (request.BodegaDestinoId == null || m.BodegaDestinoId == request.BodegaDestinoId))
            .OrderByDescending(m => m.Id)
            .ProjectTo<MovimientoBodegaDto>(_mapper.ConfigurationProvider)
            , request.PageNumber, request.PageSize);
    }

    private static readonly int?[] ESTADOS = new int?[] { EstadosMovimientoBodegaConstants.PROGRAMADO.Id, EstadosMovimientoBodegaConstants.CARGADO.Id, EstadosMovimientoBodegaConstants.ENTREGADO.Id };

}