namespace seminario.Domain.Common;

public abstract class AuditableEntity
{

    public string? Status { get; set; } = "A";

    public DateTime? Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}
