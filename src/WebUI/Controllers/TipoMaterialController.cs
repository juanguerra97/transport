using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.TipoMateriales.Queries;
using seminario.Application.TipoMateriales.Queries.GetTipoMateriales;

namespace seminario.WebUI.Controllers;
[Authorize]
public class TipoMaterialController : ApiControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<TipoMaterialDto>>> GetTipoMateriales()
    {
        return await Mediator.Send(new GetTipoMaterialesQuery());
    }
}
