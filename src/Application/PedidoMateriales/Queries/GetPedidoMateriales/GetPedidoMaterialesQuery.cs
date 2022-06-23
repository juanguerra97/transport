using System.Globalization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Constants;
using seminario.Application.Common.Interfaces;
using seminario.Application.Common.Models;

namespace seminario.Application.PedidoMateriales.Queries.GetPedidoMateriales;
public record GetPedidoMaterialesQuery : IRequest<PaginatedList<PedidoMaterialDto>>
{
    public int? BodegaSolicitaId { get; init; }
    public string? DescripcionMaterial { get; set; }
    public string? FechaDel { get; set; }
    public string? FechaAl { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public bool MostrarCreados { get; set; } = true;
    public bool MostrarAnulados { get; set; } = true;
}

public class GetPedidoMaterialesQueryHandler : IRequestHandler<GetPedidoMaterialesQuery, PaginatedList<PedidoMaterialDto>>
{

    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPedidoMaterialesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<PedidoMaterialDto>> Handle(GetPedidoMaterialesQuery request, CancellationToken cancellationToken)
    {
        string descripcionLike = "%" + request.DescripcionMaterial?.Replace(" ", "%")?.ToUpper() + "%";
        DateTime? fechaDel = ToLocalDate(request.FechaDel);
        DateTime? fechaAl = ToLocalDate(request.FechaAl);

        return await PaginatedList<PedidoMaterialDto>.CreateAsync(
            _context.PedidoMaterial
            .Where(pm => pm.Status == "A" && (request.BodegaSolicitaId == null || pm.BodegaSolicitaId == request.BodegaSolicitaId)
                && (request.DescripcionMaterial == null || EF.Functions.Like(pm.Material.Descripcion.ToUpper(), descripcionLike))
                && (fechaDel == null || pm.FechaSolicitado.Value.Date >= fechaDel.Value.Date)
                && (fechaAl == null || pm.FechaSolicitado.Value.Date <= fechaAl.Value.Date)
                && (request.MostrarCreados == true || pm.EstadoPedidoMaterialId != EstadosPedidoMaterialConstants.CREADO.Id)
                && (request.MostrarAnulados == true || pm.EstadoPedidoMaterialId != EstadosPedidoMaterialConstants.ANULADO.Id))
            .OrderByDescending(pm => pm.Id)
            .ProjectTo<PedidoMaterialDto>(_mapper.ConfigurationProvider)
            , request.PageNumber, request.PageSize);

    }

    private static DateTime? ToLocalDate(string? dateStr)
    {
        if (dateStr == null)
        {
            return null;
        }
        try
        {
            var date = DateTime.ParseExact(dateStr, "dd/MM/yyyy", CultureInfo.GetCultureInfo("es-GT"));
            return date;
        } catch(FormatException)
        {
            return null;
        } catch (CultureNotFoundException)
        {
            return null;
        }
    }
}