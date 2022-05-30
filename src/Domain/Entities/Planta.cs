namespace seminario.Domain.Entities;
public class Planta : AuditableEntity
{
    public static readonly int MAX_DESCRIPCION_LENGTH = 256;
    public static readonly int MAX_DETALLE_LENGTH = 1024;

    public int? Id { get; set; }

    public int? TipoPlantaId { get; set; }
    public TipoPlanta? TipoPlanta { get; set; }

    public string? Descripcion { get; set; }

    public string? Detalle { get; set; }

    public int? BodegaId { get; set; }
    public Bodega? Bodega { get; set; }

}
