using seminario.Application.Common.Mappings;
using seminario.Application.Departamentos.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Municipios.Queries;
public class MunicipioDto : IMapFrom<Municipio>
{
    public int? Id { get; set; }
    public string? Descripcion { get; set; }
    public DepartamentoDto? Departamento { get; set; }
}
