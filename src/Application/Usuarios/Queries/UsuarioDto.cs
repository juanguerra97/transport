
using seminario.Application.Common.Mappings;
using seminario.Domain.Entities;

namespace seminario.Application.Usuarios.Queries;
public class UsuarioDto : IMapFrom<ApplicationUser>
{
    public string Id { get; set; }
    public string UserName { get; set; } 
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
}
