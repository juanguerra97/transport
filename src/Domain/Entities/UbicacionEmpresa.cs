
namespace seminario.Domain.Entities;
public class UbicacionEmpresa : AuditableEntity
{
    public int? UbicacionId { get; set; }
    public Ubicacion? Ubicacion { get; set; }

    public int? EmpresaId { get; set; }
    public Empresa? Empresa { get; set; }

}
