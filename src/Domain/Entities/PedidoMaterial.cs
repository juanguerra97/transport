
namespace seminario.Domain.Entities;
public class PedidoMaterial : AuditableEntity
{
    public static readonly int MAX_DETALLE_LENGTH = 1024;

    public int? Id { get; set; }

    public int? EstadoPedidoMaterialId { get; set; }
    public EstadoPedidoMaterial? EstadoPedidoMaterial { get; set; }

    public int? BodegaSolicitaId { get; set; }
    public Bodega? BodegaSolicita { get; set; }

    public string? Detalle { get; set; }

    public int? MaterialId { get; set; }
    public Material? Material { get; set; }

    public double? Cantidad { get; set; }

    public DateTime? FechaSolicitado { get; set; }

    public DateTime? FechaAprobado { get; set; }

    public DateTime? FechaCompletado { get; set; }


}
