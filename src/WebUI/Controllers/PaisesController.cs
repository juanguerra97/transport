using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.Paises.Queries;

namespace seminario.WebUI.Controllers;
[Authorize]
public class PaisesController : ApiControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<PaisDto>>> GetPaises()
    {
        return await Mediator.Send(new GetPaisesQuery());
    }
}
