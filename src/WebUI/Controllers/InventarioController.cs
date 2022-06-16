using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.Common.Models;
using seminario.Application.IngresoMateriales.Commands.CreateIngresoMaterialCommand;
using seminario.Application.InventarioBodegas.Queries;
using seminario.Application.InventarioBodegas.Queries.GetInventariosByBodega;

namespace seminario.WebUI.Controllers;

[Authorize(Policy = "AdminBodega")]
public class InventarioController : ApiControllerBase
{

    [HttpGet("bodega/{bodegaId}")]
    public async Task<ActionResult<PaginatedList<InventarioBodegaDto>>> GetInventarioByBodega(
        int bodegaId, int pageSize = 10, int pageNumber = 1, string? descripcionMaterial = null)
    {
        return await Mediator.Send(new GetInventariosByBodegaQuery
        {
            BodegaId = bodegaId,
            DescripcionMaterial = descripcionMaterial,
            PageSize = pageSize,
            PageNumber = pageNumber,
        });
    }


    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateIngresoMaterialCommand command)
    {
        return await Mediator.Send(command);
    }

}
