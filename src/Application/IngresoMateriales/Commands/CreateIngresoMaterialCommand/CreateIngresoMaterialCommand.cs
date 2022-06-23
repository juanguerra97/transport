using MediatR;
using Microsoft.EntityFrameworkCore;
using seminario.Application.Common.Exceptions;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Entities;

namespace seminario.Application.IngresoMateriales.Commands.CreateIngresoMaterialCommand;

public record CreateIngresoMaterialCommand : IRequest<int?>
{
    public int? ProveedorMaterialId { get; init; }
    public int? BodegaId { get; init; }
    public int? MaterialId { get; init; }
    public double? Cantidad { get; set; }
}

public class CreateIngresoMaterialCommandHandler : IRequestHandler<CreateIngresoMaterialCommand, int?>
{
    private readonly IApplicationDbContext _context;

    public CreateIngresoMaterialCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int?> Handle(CreateIngresoMaterialCommand request, CancellationToken cancellationToken)
    {
        var proveedor = await _context.ProveedorMaterial
            .FirstOrDefaultAsync(p => p.Id == request.ProveedorMaterialId && p.Status == "A", cancellationToken);
        if (proveedor == null)
        {
            throw new NotFoundException(nameof(ProveedorMaterial), request.ProveedorMaterialId);
        }

        var bodega = await _context.Bodega
            .FirstOrDefaultAsync(b => b.Id == request.BodegaId && b.Status == "A", cancellationToken);
        if (bodega == null)
        {
            throw new NotFoundException(nameof(Bodega), request.BodegaId);
        }

        var material = await _context.Material
            .FirstOrDefaultAsync(m => m.Id == request.MaterialId && m.Status == "A", cancellationToken);
        if (material == null)
        {
            throw new NotFoundException(nameof(Material), request.MaterialId);
        }

        var entity = new IngresoMaterial
        {
            ProveedorMaterial = proveedor,
            Bodega = bodega,
            Material = material,
            Cantidad = request.Cantidad
        };

        await _context.IngresoMaterial.AddAsync(entity, cancellationToken);

        var inventario = await _context.InventarioBodega
            .FirstOrDefaultAsync(ib => ib.BodegaId == bodega.Id && ib.MaterialId == material.Id, cancellationToken);

        if (inventario == null)
        {
            inventario = new InventarioBodega
            {
                Bodega = bodega,
                Material = material,
                CantidadDisponible = entity.Cantidad,
                CantidadReservada = 0
            };
            await _context.InventarioBodega.AddAsync(inventario, cancellationToken);
        } else
        {
            inventario.CantidadDisponible += entity.Cantidad;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;

    }
}