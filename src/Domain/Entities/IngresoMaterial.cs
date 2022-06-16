namespace seminario.Domain.Entities;

public class IngresoMaterial : AuditableEntity
{
    public int? Id { get; set; }

    public int? ProveedorMaterialId { get; set; }
    public ProveedorMaterial? ProveedorMaterial { get; set; }

    public int? BodegaId { get; set; }
    public Bodega? Bodega { get; set; }

    public int? MaterialId { get; set; }
    public Material? Material { get; set; }

    public double? Cantidad { get; set; }
}
