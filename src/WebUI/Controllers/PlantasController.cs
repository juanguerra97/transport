using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.Plantas.Commands.CreatePlanta;
using seminario.Application.Plantas.Commands.DeletePlanta;
using seminario.Application.Plantas.Commands.UpdatePlanta;
using seminario.Application.Plantas.Queries;
using seminario.Application.Plantas.Queries.GetPlantaById;
using seminario.Application.Plantas.Queries.GetPlantas;

namespace seminario.WebUI.Controllers;

[Authorize(Policy = "AdminCatalogo")]
public class PlantasController : ApiControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<PlantaDto>>> GetPlantas()
    {
        return await Mediator.Send(new GetPlantasQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlantaDto>> GetPlantaById(int id)
    {
        return await Mediator.Send(new GetPlantaByIdQuery
        {
            PlantaId = id
        });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreatePlantaCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PlantaDto>> Update(int id, UpdatePlantaCommand command)
    {
        command.PlantaId = id;
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeletePlantaCommand
        {
            PlantaId = id
        });

        return NoContent();
    }

}