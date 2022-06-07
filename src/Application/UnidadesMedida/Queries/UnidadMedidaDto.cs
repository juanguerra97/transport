using seminario.Application.Common.Mappings;
using seminario.Domain.Entities;

namespace seminario.Application.UnidadesMedida.Queries;
public class UnidadMedidaDto : IMapFrom<UnidadMedida>
{
    public int? Id { get; set; }

    public string? Descripcion { get; set; }

    public string? DescripcionPlural { get; set; }

    public string? DescripcionCorta { get; set; }
}
