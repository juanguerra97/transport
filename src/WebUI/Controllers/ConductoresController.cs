using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.Common.Models;
using seminario.Application.Conductores.Commands.ActivarConductor;
using seminario.Application.Conductores.Commands.CreateConductor;
using seminario.Application.Conductores.Commands.DeleteConductor;
using seminario.Application.Conductores.Commands.InactivarConductor;
using seminario.Application.Conductores.Commands.UpdateConductor;
using seminario.Application.Conductores.Queries;
using seminario.Application.Conductores.Queries.GetConductorById;
using seminario.Application.Conductores.Queries.GetConductores;

namespace seminario.WebUI.Controllers;

[Authorize(Policy = "AdminCatalogo")]
public class ConductoresController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<ConductorDto>>> GetConductores(
        int pageSize = 10, int pageNumber = 1, string? nombre = null)
    {
        return await Mediator.Send(new GetConductoresQuery
        {
            PageSize = pageSize,
            PageNumber = pageNumber,
            Nombre = nombre
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ConductorDto>> GetConductorById(int id)
    {
        return await Mediator.Send(new GetConductorByIdQuery
        {
            ConductorId = id
        });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateConductorCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ConductorDto>> Update(int id, UpdateConductorCommand command)
    {
        command.ConductorId = id;
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteConductorCommand
        {
            ConductorId = id
        });
        return NoContent();
    }

    [HttpPost("inactivar/{id}")]
    public async Task<ActionResult> Inactivar(int id)
    {
        await Mediator.Send(new InactivarConductorCommand { ConductorId = id });
        return NoContent();
    }

    [HttpPost("activar/{id}")]
    public async Task<ActionResult> Activar(int id)
    {
        await Mediator.Send(new ActivarConductorCommand { ConductorId = id });
        return NoContent();
    }
}