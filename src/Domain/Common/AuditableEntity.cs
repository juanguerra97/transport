namespace seminario.Domain.Common;

public abstract class AuditableEntity
{

    public string? Status { get; set; } = "A";

    public DateTime? FechaInsert { get; set; }

    public string? UsuarioInsert { get; set; }

    public DateTime? FechaUpdate { get; set; }

    public string? UsuarioUpdate { get; set; }
}
