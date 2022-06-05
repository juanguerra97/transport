using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.TipoPlantas.Queries;
using seminario.Application.TipoPlantas.Queries.GetTipoPlantas;

namespace seminario.WebUI.Controllers;

[Authorize]
public class TipoPlantaController : ApiControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<TipoPlantaDto>>> GetTipoPlantas()
    {
        return await Mediator.Send(new GetTipoPlantasQuery());
    }
}
