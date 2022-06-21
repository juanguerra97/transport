using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.MovimientosBodega.Queries;
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

}
