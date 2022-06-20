using seminario.Application.Common.Mappings;
using seminario.Application.Usuarios.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Conductores.Queries;

public class ConductorDto : IMapFrom<Conductor>
{
    public int? Id { get; set; }
    public UsuarioDto? User { get; set; }
    public string? NoLicencia { get; set; }
    public string? Status { get; set; }
}
