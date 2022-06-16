using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.Common.Models;
using seminario.Application.Proveedores.Commands.CreateProveedorCommand;
using seminario.Application.Proveedores.Commands.DeleteProveedorCommand;
using seminario.Application.Proveedores.Commands.UpdateProveedorCommand;
using seminario.Application.Proveedores.Queries;
using seminario.Application.Proveedores.Queries.GetProveedorById;
using seminario.Application.Proveedores.Queries.GetProveedores;
using seminario.Application.Proveedores.Queries.SearchProveedorByName;

namespace seminario.WebUI.Controllers;
[Authorize(Policy = "AdminCatalogo")]
public class ProveedoresController : ApiControllerBase
{

    [HttpGet]
    public async Task<ActionResult<PaginatedList<ProveedorDto>>> GetProveedores(
        int pageSize = 10, int pageNumber = 1, string? nombre = null, string? nit = null, 
        string? telefono = null, string? email = null)
    {
        return await Mediator.Send(new GetProveedoresQuery
        {
            PageSize = pageSize,
            PageNumber = pageNumber,
            Nombre = nombre,
            Nit = nit,
            Email = email,
            Telefono = telefono,
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProveedorDto>> GetProveedorById(int id)
    {
        return await Mediator.Send(new GetProveedorByIdQuery
        {
            ProveedorId = id
        });
    }

    [HttpGet("searchByName")]
    public async Task<ActionResult<List<ProveedorDto>>> SearchProveedoresByName(string name, int maxResults = 5)
    {
        return await Mediator.Send(new SearchProveedorByNameQuery
        {
            Name = name,
            MaxResults = maxResults
        });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateProveedorCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProveedorDto>> Update(int id, UpdateProveedorCommand command)
    {
        command.ProveedorId = id;
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteProveedorCommand
        {
            ProveedorId = id
        });
        return NoContent();
    }
}
