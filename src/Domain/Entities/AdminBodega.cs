
namespace seminario.Domain.Entities;
public class AdminBodega : AuditableEntity
{
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }

    public int? BodegaId { get; set; }
    public Bodega? Bodega { get; set; }

}
