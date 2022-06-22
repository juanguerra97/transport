using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.Common.Models;
using seminario.Application.MovimientosBodega.Queries;
using seminario.Application.MovimientosBodega.Queries.GetMovimientosBodegaByConductor;
using seminario.Application.MovimientosBodega.Queries.GetMovimientosBodegaByPedido;

namespace seminario.WebUI.Controllers;

public class MovimientoBodegaController : ApiControllerBase
{

    [HttpGet("byPedido/{pedidoMaterialId}")]
    [Authorize(Policy = "AdminPedidos")]
    public async Task<ActionResult<List<MovimientoBodegaDto>>> GetMovimientosBodegaByPedido(int pedidoMaterialId)
    {
        return await Mediator.Send(new GetMovimientosBodegaByPedidoQuery {
            PedidoMaterialId = pedidoMaterialId,
        });
    }

    [HttpGet("byConductor")]
    [Authorize(Policy = "Conductor")]
    public async Task<ActionResult<PaginatedList<MovimientoBodegaDto>>> GetMovimientosBodegaByConductor(
        int pageSize = 10, int pageNumber = 1, string? descripcionMaterial = null, int? bodegaOrigenId = null, int? bodegaDestinoId = null)
    {
        return await Mediator.Send(new GetMovimientosBodegaByConductorQuery
        {
            PageSize = pageSize,
            PageNumber = pageNumber,
            DescripcionMaterial = descripcionMaterial,
            BodegaOrigenId = bodegaOrigenId,
            BodegaDestinoId = bodegaDestinoId,
        });
    }

}
