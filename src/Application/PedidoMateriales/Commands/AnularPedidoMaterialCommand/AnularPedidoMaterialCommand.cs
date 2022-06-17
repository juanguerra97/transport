using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Constants;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.PedidoMateriales.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.PedidoMateriales.Commands.AnularPedidoMaterialCommand;
public record AnularPedidoMaterialCommand : IRequest<PedidoMaterialDto>
{
    public int PedidoMaterialId { get; init; }
}

public class AnularPedidoMaterialCommandHandler : IRequestHandler<AnularPedidoMaterialCommand, PedidoMaterialDto>
{

    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AnularPedidoMaterialCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PedidoMaterialDto> Handle(AnularPedidoMaterialCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.PedidoMateriales
            .Include(pm => pm.EstadoPedidoMaterial)
            .Include(pm => pm.Material)
            .ThenInclude(pm => pm.UnidadMedida)
            .FirstOrDefaultAsync(pm => pm.Id == request.PedidoMaterialId && pm.Status == "A", cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(PedidoMaterial), request.PedidoMaterialId);
        }

        if (entity.EstadoPedidoMaterialId != EstadosPedidoMaterialConstants.CREADO.Id
            && entity.EstadoPedidoMaterialId != EstadosPedidoMaterialConstants.PENDIENTE.Id)
        {
            throw new CustomValidationException($"El pedido esta en estado {entity.EstadoPedidoMaterial.Descripcion} y no puede ser anulado.");
        }

        entity.EstadoPedidoMaterialId = EstadosPedidoMaterialConstants.ANULADO.Id;
        await _context.BitacoraEstadoPedidoMateriales.AddAsync(new BitacoraEstadoPedidoMaterial
        {
            PedidoMaterial = entity,
            EstadoPedidoMaterialId = EstadosPedidoMaterialConstants.ANULADO.Id
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PedidoMaterial, PedidoMaterialDto>(entity);
    }
}