
namespace seminario.Domain.Entities;
public class AdminEmpresa : AuditableEntity
{
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }

    public int? EmpresaId { get; set; }
    public Empresa? Empresa { get; set; }
}
