using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Constants;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Application.PedidoMateriales.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.PedidoMateriales.Commands.UpdatePedidoMaterialCommand;
public record UpdatePedidoMaterialCommand : IRequest<PedidoMaterialDto>
{
    public int? PedidoMaterialId { get; set; }
    public string? Detalle { get; init; }
    public double? Cantidad { get; init; }
}

public class UpdatePedidoMaterialCommandHandler : IRequestHandler<UpdatePedidoMaterialCommand, PedidoMaterialDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdatePedidoMaterialCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PedidoMaterialDto> Handle(UpdatePedidoMaterialCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.PedidoMaterial
             .Include(pm => pm.EstadoPedidoMaterial)
             .Include(pm => pm.Material)
             .ThenInclude(pm => pm.UnidadMedida)
             .FirstOrDefaultAsync(pm => pm.Id == request.PedidoMaterialId && pm.Status == "A", cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(PedidoMaterial), request.PedidoMaterialId);
        }

        if (entity.EstadoPedidoMaterialId != EstadosPedidoMaterialConstants.CREADO.Id)
        {
            throw new CustomValidationException($"El pedido esta en estado {entity.EstadoPedidoMaterial.Descripcion} y no puede ser modificado.");
        }

        entity.Detalle = request.Detalle;
        entity.Cantidad = request.Cantidad;

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PedidoMaterial, PedidoMaterialDto>(entity);
    }
}