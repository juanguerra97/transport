using seminario.Application.Common.Mappings;
using seminario.Domain.Entities;

namespace seminario.Application.Proveedores.Queries;
public class ProveedorDto : IMapFrom<ProveedorMaterial>
{
    public int? Id { get; set; }
    public string? Nombre { get; set; }
    public string? Nit { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public string? Direccion { get; set; }
}
