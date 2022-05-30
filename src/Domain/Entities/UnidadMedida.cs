
namespace seminario.Domain.Entities;
public class UnidadMedida : AuditableEntity
{
    public static readonly int MAX_DESCRIPCION_LENGTH = 128;
    public static readonly int MAX_DESCRIPCIONPLURAL_LENGTH = 128;
    public static readonly int MAX_DESCRIPCIONCORTA_LENGTH = 128;

    public int? Id { get; set; }

    public string? Descripcion { get; set; }

    public string? DescripcionPlural { get; set; }

    public string? DescripcionCorta { get; set; }
}
