
namespace seminario.Domain.Entities;
public class Bodega : AuditableEntity
{
    public static readonly int MAX_DESCRIPCION_LENGTH = 256;
    public static readonly int MAX_DETALLE_LENGTH = 1024;

    public int? Id { get; set; }

    public string? Descripcion { get; set; }

    public string? Detalle { get; set; }

    public int? UbicacionId { get; set; }
    public Ubicacion? Ubicacion { get; set; }


}
