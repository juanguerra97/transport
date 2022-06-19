using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.PedidoMateriales.Queries.GetPedidoById;
public record GetPedidoByIdQuery : IRequest<PedidoMaterialDto>
{
    public int PedidoMaterialId { get; init; }
}

public class GetPedidoByIdQueryHandler : IRequestHandler<GetPedidoByIdQuery, PedidoMaterialDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPedidoByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PedidoMaterialDto> Handle(GetPedidoByIdQuery request, CancellationToken cancellationToken)
    {

        var entity = await _context.PedidoMateriales
            .Include(pm => pm.EstadoPedidoMaterial)
            .Include(pm => pm.Material)
            .ThenInclude(m => m.TipoMaterial)
            .Include(pm => pm.Material)
            .ThenInclude(m => m.UnidadMedida)
            .Include(pm => pm.BodegaSolicita)
            .ThenInclude(b => b.AdminBodega)
            .ThenInclude(ab => ab.User)
            .Include(pm => pm.BodegaSolicita)
            .ThenInclude(b => b.Ubicacion)
            .ThenInclude(u => u.Municipio)
            .ThenInclude(m => m.Departamento)
            .ThenInclude(d => d.Pais)
            .FirstOrDefaultAsync(pm => pm.Id == request.PedidoMaterialId && pm.Status == "A", cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(PedidoMaterial), request.PedidoMaterialId);
        }

        return _mapper.Map<PedidoMaterial, PedidoMaterialDto>(entity);
    }
}
