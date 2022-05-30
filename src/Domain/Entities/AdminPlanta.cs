
namespace seminario.Domain.Entities;
public class AdminPlanta : AuditableEntity
{
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }

    public int? PlantaId { get; set; }
    public Planta? Planta { get; set; }
}
