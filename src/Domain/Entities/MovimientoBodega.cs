
namespace seminario.Domain.Entities;
public class MovimientoBodega : AuditableEntity
{
    public static readonly int MAX_DETALLE_LENGTH = 1024;

    public int? Id { get; set; }
    public int? EstadoMovimientoBodegaId { get; set; }
    public EstadoMovimientoBodega? EstadoMovimientoBodega { get; set; }

    public int? PedidoMaterialId { get; set; }
    public PedidoMaterial? PedidoMaterial { get; set; }

    public int? BodegaOrigenId { get; set; }
    public Bodega? BodegaOrigen { get; set; }

    public int? BodegaDestinoId { get; set; }
    public Bodega? BodegaDestino { get; set; }

    public DateTime? FechaInicioProgramado { get; set; }

    public DateTime? FechaCargado { get; set; }

    public DateTime? FechaDescargado { get; set; }

    public int? MaterialId { get; set; }
    public Material? Material { get; set; }

    public double? Cantidad { get; set; }

    public string? Detalle { get; set; }

    public int? VehiculoId { get; set; }
    public Vehiculo? Vehiculo { get; set; }

    public string? ConductorId { get; set; }
    public Conductor? Conductor { get; set; }

}
