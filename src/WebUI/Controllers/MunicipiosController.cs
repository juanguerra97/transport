using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.Municipios.Queries;
using seminario.Application.Municipios.Queries.GetMunicipiosQuery;

namespace seminario.WebUI.Controllers;
[Authorize]
public class MunicipiosController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<MunicipioDto>>> GetMunicipios([FromQuery] int? paisId, [FromQuery] int? departamentoId)
    {
        return await Mediator.Send(new GetMunicipiosQuery {
            PaisId = paisId,
            DepartamentoId = departamentoId,
        });
    }
}
