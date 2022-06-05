
using seminario.Application.Common.Mappings;
using seminario.Application.Paises.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Departamentos.Queries;
public class DepartamentoDto : IMapFrom<Departamento>
{
    public int? Id { get; set; }
    public string? Descripcion { get; set; }
    public PaisDto? Pais { get; set; }
}
