using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.Common.Models;
using seminario.Application.Vehiculos.Commands.ActivarVehiculo;
using seminario.Application.Vehiculos.Commands.CreateVehiculo;
using seminario.Application.Vehiculos.Commands.DeleteVehiculo;
using seminario.Application.Vehiculos.Commands.InactivarVehiculo;
using seminario.Application.Vehiculos.Commands.UpdateVehiculo;
using seminario.Application.Vehiculos.Queries;
using seminario.Application.Vehiculos.Queries.GetVehiculoById;
using seminario.Application.Vehiculos.Queries.GetVehiculos;

namespace seminario.WebUI.Controllers;
[Authorize(Policy = "AdminCatalogo")]
public class VehiculosController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<VehiculoDto>>> GetVehiculos(
        int pageSize = 10, int pageNumber = 1, string? descripcion = null, string? codigo = null, string? placa = null, string? status = null)
    {
        return await Mediator.Send(new GetVehiculosQuery
        {
            PageSize = pageSize,
            PageNumber = pageNumber,
            Descripcion = descripcion,
            Codigo = codigo,
            Placa = placa,
            Status = status,
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VehiculoDto>> GetVehiculoById(int id)
    {
        return await Mediator.Send(new GetVehiculoByIdQuery
        {
            VehiculoId = id
        });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateVehiculoCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<VehiculoDto>> Update(int id, UpdateVehiculoCommand command)
    {
        command.VehiculoId = id;
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteVehiculoCommand
        {
            VehiculoId = id
        });
        return NoContent();
    }

    [HttpPost("inactivar/{id}")]
    public async Task<ActionResult> Inactivar(int id)
    {
        await Mediator.Send(new InactivarVehiculoCommand { VehiculoId = id });
        return NoContent();
    }

    [HttpPost("activar/{id}")]
    public async Task<ActionResult> Activar(int id)
    {
        await Mediator.Send(new ActivarVehiculoCommand { VehiculoId = id });
        return NoContent();
    }

}
