using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.Bodegas.Commands.CreateBodega;
using seminario.Application.Bodegas.Commands.DeleteBodega;
using seminario.Application.Bodegas.Commands.UpdateBodega;
using seminario.Application.Bodegas.Queries;
using seminario.Application.Bodegas.Queries.GetBodegaById;
using seminario.Application.Bodegas.Queries.GetBodegas;

namespace seminario.WebUI.Controllers;
[Authorize]
public class BodegasController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<BodegaDto>>> GetBodegas()
    {
        return await Mediator.Send(new GetBodegasQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BodegaDto>> GetBodegaById(int id)
    {
        return await Mediator.Send(new GetBodegaByIdQuery
        {
            BodegaId = id
        });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateBodegaCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BodegaDto>> Update(int id, UpdateBodegaCommand command)
    {
        command.BodegaId = id;
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteBodegaCommand
        {
            BodegaId = id
        });

        return NoContent();
    }

}
