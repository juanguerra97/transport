using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using seminario.Application.Bodegas.Commands.CreateBodega;
using seminario.Application.Bodegas.Commands.DeleteBodega;
using seminario.Application.Bodegas.Commands.UpdateBodega;
using seminario.Application.Bodegas.Queries;
using seminario.Application.Bodegas.Queries.GetBodegaById;
using seminario.Application.Bodegas.Queries.GetBodegas;
using seminario.Application.Bodegas.Queries.GetBodegasByEncargado;
using seminario.Application.Common.Interfaces;
using seminario.Application.Common.Models;

namespace seminario.WebUI.Controllers;

public class BodegasController : ApiControllerBase
{
    private readonly ICurrentUserService _currenUserService;

    public BodegasController(ICurrentUserService currentUserService)
    {
        _currenUserService = currentUserService;
    }

    [HttpGet]
    [Authorize(Policy = "AdminCatalogo")]
    public async Task<ActionResult<List<BodegaDto>>> GetBodegas()
    {
        return await Mediator.Send(new GetBodegasQuery
        {
            IncludePlantas = false
        });
    }

    [HttpGet("all")]
    [Authorize(Policy = "AdminPedidos")]
    public async Task<ActionResult<List<BodegaDto>>> GetAllBodegas()
    {
        return await Mediator.Send(new GetBodegasQuery
        {
            IncludePlantas = true
        });
    }

    [HttpGet("ByEncargado")]
    [Authorize(Policy = "AdminBodega")]
    public async Task<ActionResult<PaginatedList<BodegaDto>>> GetBodegasByEncargado(int pageSize = 10, int pageNumber = 1)
    {
        return await Mediator.Send(new GetBodegasByEncargadoQuery
        {
            PageSize = pageSize,
            PageNumber = pageNumber,
            EncargadoId = _currenUserService.UserId
        });
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "AdminCatalogo")]
    public async Task<ActionResult<BodegaDto>> GetBodegaById(int id)
    {
        return await Mediator.Send(new GetBodegaByIdQuery
        {
            BodegaId = id
        });
    }

    [HttpPost]
    [Authorize(Policy = "AdminCatalogo")]
    public async Task<ActionResult<int>> Create(CreateBodegaCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminCatalogo")]
    public async Task<ActionResult<BodegaDto>> Update(int id, UpdateBodegaCommand command)
    {
        command.BodegaId = id;
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminCatalogo")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteBodegaCommand
        {
            BodegaId = id
        });

        return NoContent();
    }

}
