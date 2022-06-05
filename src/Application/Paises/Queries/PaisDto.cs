using seminario.Application.Common.Mappings;
using seminario.Domain.Entities;

namespace seminario.Application.Paises.Queries;
public class PaisDto : IMapFrom<Pais>
{
    public int? Id { get; set; }
    public string? Descripcion { get; set; }
}