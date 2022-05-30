
namespace seminario.Domain.Entities;
public class Empresa : AuditableEntity
{
    public static readonly int MAX_DESCRIPCION_LENGTH = 256;
    public static readonly int MAX_DETALLE_LENGTH = 1024;
    public static readonly int MAX_NIT_LENGTH = 32;

    public int? Id { get; set; }

    public int? TipoEmpresaId { get; set; }

    public TipoEmpresa? TipoEmpresa { get; set; }

    public string? Descripcion { get; set; }

    public string? Detalle { get; set; }

    public string? Nit { get; set; }

}
