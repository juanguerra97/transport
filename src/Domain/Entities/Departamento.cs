
namespace seminario.Domain.Entities;
public class Departamento : AuditableEntity
{
    public static readonly int MAX_DESCRIPCION_LENGTH = 256;

    public int? Id { get; set; }
    public string? Descripcion { get; set; }

    public int? PaisId { get; set; }
    public Pais? Pais { get; set; }
}
