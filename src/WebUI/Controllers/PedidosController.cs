using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.Common.Models;
using seminario.Application.PedidoMateriales.Commands.AnularPedidoMaterialCommand;
using seminario.Application.PedidoMateriales.Commands.CreatePedidoMaterialCommand;
using seminario.Application.PedidoMateriales.Commands.EnviarPedidoMaterialCommand;
using seminario.Application.PedidoMateriales.Commands.UpdatePedidoMaterialCommand;
using seminario.Application.PedidoMateriales.Queries;
using seminario.Application.PedidoMateriales.Queries.GetPedidoById;
using seminario.Application.PedidoMateriales.Queries.GetPedidoMateriales;

namespace seminario.WebUI.Controllers;

[Authorize]
public class PedidosController : ApiControllerBase
{

    [HttpGet]
    [Authorize(Policy = "AdminPedidos")]
    public async Task<ActionResult<PaginatedList<PedidoMaterialDto>>> GetPedidos(
        int pageSize = 10, int pageNumber = 1, int? bodegaId = null, string? descripcionMaterial = null, string? fechaDel = null, string? fechaAl = null)
    {
        return await Mediator.Send(new GetPedidoMaterialesQuery
        {
            BodegaSolicitaId = bodegaId,
            DescripcionMaterial = descripcionMaterial,
            FechaDel = fechaDel,
            FechaAl = fechaAl,
            PageSize = pageSize,
            PageNumber = pageNumber,
            MostrarAnulados = false,
            MostrarCreados = false,
        });
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "AdminPedidos")]
    public async Task<ActionResult<PedidoMaterialDto>> GetPedidoById(int id)
    {
        return await Mediator.Send(new GetPedidoByIdQuery
        {
            PedidoMaterialId = id
        });
    }

    [HttpGet("bodega/{bodegaId}")]
    [Authorize(Policy = "AdminBodega")]
    public async Task<ActionResult<PaginatedList<PedidoMaterialDto>>> GetPedidosByBodega(
        int bodegaId, int pageSize = 10, int pageNumber = 1, string? descripcionMaterial = null, string? fechaDel = null, string? fechaAl = null)
    {
        return await Mediator.Send(new GetPedidoMaterialesQuery
        {
            BodegaSolicitaId = bodegaId,
            DescripcionMaterial = descripcionMaterial,
            FechaDel = fechaDel,
            FechaAl = fechaAl,
            PageSize = pageSize,
            PageNumber = pageNumber,
            MostrarAnulados = true,
            MostrarCreados = true,
        });
    }

    [HttpPost]
    [Authorize(Policy = "AdminBodega")]
    public async Task<ActionResult<int>> Create(CreatePedidoMaterialCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminBodega")]
    public async Task<ActionResult<PedidoMaterialDto>> Update(int id, UpdatePedidoMaterialCommand command)
    {
        command.PedidoMaterialId = id;
        return await Mediator.Send(command);
    }

    [HttpPost("enviar/{pedidoMaterialId}")]
    [Authorize(Policy = "AdminBodega")]
    public async Task<ActionResult> EnviarPedido(int pedidoMaterialId)
    {
        await Mediator.Send(new EnviarPedidoMaterialCommand
        {
            PedidoMaterialId = pedidoMaterialId
        });
        return NoContent();
    }

    [HttpPost("anular/{pedidoMaterialId}")]
    [Authorize(Policy = "AdminBodega")]
    public async Task<ActionResult> AnularPedido(int pedidoMaterialId)
    {
        await Mediator.Send(new AnularPedidoMaterialCommand
        {
            PedidoMaterialId = pedidoMaterialId
        });
        return NoContent();
    }

}
