using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Constants;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.PedidoMateriales.Commands.CreatePedidoMaterialCommand;

public record CreatePedidoMaterialCommand : IRequest<int?>
{
    public int? BodegaSolicitaId { get; init; }
    public string? Detalle { get; set; }
    public int? MaterialId { get; set; }
    public double? Cantidad { get; set; }

}

public class CreatePedidoMaterialCommandHandler : IRequestHandler<CreatePedidoMaterialCommand, int?>
{
    private readonly IApplicationDbContext _context;

    public CreatePedidoMaterialCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int?> Handle(CreatePedidoMaterialCommand request, CancellationToken cancellationToken)
    {

        var bodega = await _context.Bodegas
            .FirstOrDefaultAsync(b => b.Id == request.BodegaSolicitaId && b.Status == "A", cancellationToken);
        if (bodega == null)
        {
            throw new NotFoundException(nameof(Bodega), request.BodegaSolicitaId);
        }

        var material = await _context.Materiales
            .FirstOrDefaultAsync(m => m.Id == request.MaterialId && m.Status == "A", cancellationToken);
        if (material == null)
        {
            throw new NotFoundException(nameof(Bodega), request.MaterialId);
        }

        if (true == (await _context.PedidoMateriales.AnyAsync(pm => pm.Status == "A" && (pm.EstadoPedidoMaterialId == EstadosPedidoMaterialConstants.CREADO.Id || pm.EstadoPedidoMaterialId == EstadosPedidoMaterialConstants.PENDIENTE.Id),cancellationToken)))
        {
            throw new CustomValidationException("Actualmente existe otro pedido del mismo material en estado CREADO o PENDIENTE.");
        }

        var entity = new PedidoMaterial
        {
            EstadoPedidoMaterialId = EstadosPedidoMaterialConstants.CREADO.Id,
            BodegaSolicita = bodega,
            Material = material,
            Detalle = request.Detalle,
            Cantidad = request.Cantidad,
            FechaSolicitado = DateTime.Now,
        };
        await _context.PedidoMateriales.AddAsync(entity, cancellationToken);

        var bitacora = new BitacoraEstadoPedidoMaterial
        {
            PedidoMaterial = entity,
            EstadoPedidoMaterialId = EstadosPedidoMaterialConstants.CREADO.Id,
        };
        await _context.BitacoraEstadoPedidoMateriales.AddAsync(bitacora, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
