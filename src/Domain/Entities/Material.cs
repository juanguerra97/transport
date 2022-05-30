
namespace seminario.Domain.Entities;
public class Material : AuditableEntity
{
    public static readonly int MAX_DESCRIPCION_LENGTH = 256;
    public static readonly int MAX_DETALLE_LENGTH = 4096;

    public int? Id { get; set; }

    public int? TipoMaterialId { get; set; }
    public TipoMaterial? TipoMaterial { get; set; }

    public string? Descripcion { get; set; }

    public string? Detalle { get; set; }

    public int? UnidadMedidaId { get; set; }
    public UnidadMedida? UnidadMedida { get; set; }

    public double? Peso { get; set; }
}
