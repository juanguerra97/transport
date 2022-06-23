using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.Common.Models;
using seminario.Application.MovimientosBodega.Commands.CargarMovimientoBodega;
using seminario.Application.MovimientosBodega.Commands.EntregarMovimientoBodega;
using seminario.Application.MovimientosBodega.Queries;
using seminario.Application.MovimientosBodega.Queries.GetMovimientosBodega;
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

    [HttpGet("byBodegaDestino/{bodegaDestinoId}")]
    [Authorize(Policy = "AdminBodega")]
    public async Task<ActionResult<PaginatedList<MovimientoBodegaDto>>> GetMovimientosByBodegaDestino(
        int bodegaDestinoId, int pageSize = 10, int pageNumber = 1, string? descripcionMaterial = null, int? bodegaOrigenId = null, int? conductorId = null, int? vehiculoId = null)
    {
        return await Mediator.Send(new GetMovimientosBodegaQuery
        {
            PageSize = pageSize,
            PageNumber = pageNumber,
            DescripcionMaterial = descripcionMaterial,
            BodegaOrigenId = bodegaOrigenId,
            BodegaDestinoId = bodegaDestinoId,
            ConductorId = conductorId,
            VehiculoId = vehiculoId,
        });
    }

    [HttpPost("cargar/{movimientoBodegaId}")]
    [Authorize(Policy = "Conductor")]
    public async Task<ActionResult<MovimientoBodegaDto>> CargarMovimiento(int movimientoBodegaId)
    {
        return await Mediator.Send(new CargarMovimientoBodegaCommand
        {
           MovimientoBodegaId = movimientoBodegaId
        });
    }

    [HttpPost("entregar/{movimientoBodegaId}")]
    [Authorize(Policy = "AdminBodega")]
    public async Task<ActionResult<MovimientoBodegaDto>> EntregarMovimiento(int movimientoBodegaId)
    {
        return await Mediator.Send(new EntregarMovimientoBodegaCommand
        {
            MovimientoBodegaId = movimientoBodegaId
        });
    }

}
