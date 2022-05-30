
namespace seminario.Domain.Entities;
public class InventarioBodega : AuditableEntity
{

    public int? BodegaId { get; set; }
    public Bodega? Bodega { get; set; }

    public int? MaterialId { get; set; }
    public Material? Material { get; set; }

    public double? CantidadDisponible { get; set; }

    public double? CantidadReservada { get; set; }

}
