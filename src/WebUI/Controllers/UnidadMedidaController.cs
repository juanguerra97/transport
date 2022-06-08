using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.UnidadesMedida.Commands.CreateUnidadMedida;
using seminario.Application.UnidadesMedida.Commands.DeleteUnidadMedida;
using seminario.Application.UnidadesMedida.Commands.UpdateUnidadMedida;
using seminario.Application.UnidadesMedida.Queries;
using seminario.Application.UnidadesMedida.Queries.GetUnidadesMedida;
using seminario.Application.UnidadesMedida.Queries.GetUnidadMedidaById;

namespace seminario.WebUI.Controllers;

[Authorize]
public class UnidadMedidaController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<UnidadMedidaDto>>> GetUnidadesMedida()
    {
        return await Mediator.Send(new GetUnidadesMedidaQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UnidadMedidaDto>> GetUnidadMedidaById(int id)
    {
        return await Mediator.Send(new GetUnidadMedidaByIdQuery
        {
            UnidadMedidaId = id
        });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateUnidadMedidaCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UnidadMedidaDto>> Update(int id, UpdateUnidadMedidaCommand command)
    {
        command.UnidadMedidaId = id;
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteUnidadMedidaCommand
        {
            UnidadMedidaId = id
        });

        return NoContent();
    }

}
