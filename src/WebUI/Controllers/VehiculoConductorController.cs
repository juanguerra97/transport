using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.Conductores.Queries;
using seminario.Application.VehiculoConductores.Commands.CreateVehiculoConductor;
using seminario.Application.VehiculoConductores.Commands.DeleteVehiculoConductor;
using seminario.Application.VehiculoConductores.Queries.GetConductoresByVehiculo;
using seminario.Application.VehiculoConductores.Queries.GetConductoresDisponiblesByVehiculo;
using seminario.Application.VehiculoConductores.Queries.GetVehiculosByConductor;
using seminario.Application.VehiculoConductores.Queries.GetVehiculosDisponiblesByConductor;
using seminario.Application.Vehiculos.Queries;

namespace seminario.WebUI.Controllers;

[Authorize(Policy = "AdminCatalogo")]
public class VehiculoConductorController : ApiControllerBase
{
    [HttpGet("vehiculo/conductores/{vehiculoId}")]
    public async Task<ActionResult<List<ConductorDto>>> GetConductoresByVehiculo(int vehiculoId)
    {
        return await Mediator.Send(new GetConductoresByVehiculoQuery
        {
            VehiculoId = vehiculoId
        });
    }

    [HttpGet("vehiculo/conductores/{vehiculoId}/disponibles")]
    public async Task<ActionResult<List<ConductorDto>>> GetConductoresDisponiblesByVehiculo(int vehiculoId)
    {
        return await Mediator.Send(new GetConductoresDisponiblesByVehiculoQuery
        {
            VehiculoId = vehiculoId
        });
    }

    [HttpGet("conductor/vehiculos/{conductorId}")]
    public async Task<ActionResult<List<VehiculoDto>>> GetVehiculosByConductor(int conductorId)
    {
        return await Mediator.Send(new GetVehiculosByConductorQuery
        {
            ConductorId = conductorId
        });
    }

    [HttpGet("conductor/vehiculos/{conductorId}/disponibles")]
    public async Task<ActionResult<List<VehiculoDto>>> GetVehiculosDisponiblesByConductor(int conductorId)
    {
        return await Mediator.Send(new GetVehiculosDisponiblesByConductorQuery
        {
            ConductorId = conductorId
        });
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateVehiculoConductorCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(DeleteVehiculoConductorCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

}
