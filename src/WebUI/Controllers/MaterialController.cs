using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.Common.Models;
using seminario.Application.Materiales.Commands.CreateMaterialCommand;
using seminario.Application.Materiales.Commands.DeleteMaterialCommand;
using seminario.Application.Materiales.Commands.UpdateMaterialCommand;
using seminario.Application.Materiales.Queries;
using seminario.Application.Materiales.Queries.GetMaterialById;
using seminario.Application.Materiales.Queries.GetMateriales;

namespace seminario.WebUI.Controllers;
[Authorize]
public class MaterialController : ApiControllerBase
{

    [HttpGet]
    public async Task<ActionResult<PaginatedList<MaterialDto>>> GetMateriales(int pageSize = 10, int pageNumber = 1, int? tipoMaterialId = null)
    {
        return await Mediator.Send(new GetMaterialesQuery { 
            PageSize = pageSize,
            PageNumber = pageNumber,
            TipoMaterialId = tipoMaterialId,
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MaterialDto>> GetMaterialById(int id)
    {
        return await Mediator.Send(new GetMaterialByIdQuery
        {
            MaterialId = id
        });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateMaterialCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MaterialDto>> Update(int id, UpdateMaterialCommand command)
    {
        command.MaterialId = id;
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteMaterialCommand
        {
            MaterialId = id
        });
        return NoContent();
    }
}
