using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.UnidadesMedida.Queries;
using seminario.Application.UnidadesMedida.Queries.GetUnidadesMedida;
using seminario.Application.UnidadesMedida.Queries.GetUnidadMedidaById;

namespace seminario.WebUI.Controllers;

[Authorize]
public class UnidadMedida : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<UnidadMedidaDto>>> GetUnidadesMedida()
    {
        return await Mediator.Send(new GetUnidadesMedidaQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UnidadMedidaDto>> GetUnidadMedidaById(int id)
    {
        return await Mediator.Send(new GetUnidadMedidaByIdQuery
        {
            UnidadMedidaId = id
        });
    }
}
