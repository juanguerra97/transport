
namespace seminario.Domain.Entities;
public class Conductor : AuditableEntity
{

    public static readonly int MAX_NOLICENCIA_LENGTH = 64;

    public int? Id { get; set; }

    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }

    public string? NoLicencia { get; set; }

}
