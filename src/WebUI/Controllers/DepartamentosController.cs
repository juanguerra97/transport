using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.Departamentos.Queries;
using seminario.Application.Departamentos.Queries.GetDepartamentos;

namespace seminario.WebUI.Controllers;
[Authorize]
public class DepartamentosController : ApiControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<DepartamentoDto>>> GetDepartamentos([FromQuery] int? paisId)
    {
        return await Mediator.Send(new GetDepartamentosQuery {
            PaisId = paisId
        });
    }

}
