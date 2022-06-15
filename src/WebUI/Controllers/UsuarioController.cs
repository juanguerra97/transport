using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.Usuarios.Queries;
using seminario.Application.Usuarios.Queries.SearchByName;

namespace seminario.WebUI.Controllers;
[Authorize(Policy = "AdminCatalogo")]
public class UsuarioController : ApiControllerBase
{

    [HttpGet("searchByName")]
    public async Task<ActionResult<List<UsuarioDto>>> SearchUsuariosByName(string name, int maxResults = 5)
    {
        return await Mediator.Send(new SearchByNameQuery
        {
            Name = name,
            MaxResults = maxResults
        });
    }

}
