
namespace seminario.Domain.Entities;
public class TipoPlanta : AuditableEntity
{
    public static readonly int MAX_DESCRIPCION_LENGTH = 64;
    public int? Id { get; set; }

    public string? Descripcion { get; set; }
}
