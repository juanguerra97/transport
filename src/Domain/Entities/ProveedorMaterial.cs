
namespace seminario.Domain.Entities;
public class ProveedorMaterial : AuditableEntity
{
    public static readonly int MAX_NOMBRE_LENGTH = 256;
    public static readonly int MAX_NIT_LENGTH = 32;
    public static readonly int MAX_TELEFONO_LENGTH = 16;
    public static readonly int MAX_EMAIL_LENGTH = 256;
    public static readonly int MAX_DIRECCION_LENGTH = 256;

    public int? Id { get; set; }

    public string? Nombre { get; set; }

    public string? Nit { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? Direccion { get; set; }
    
}
