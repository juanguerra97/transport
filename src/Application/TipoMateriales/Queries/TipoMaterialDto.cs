using seminario.Application.Common.Mappings;
using seminario.Domain.Entities;

namespace seminario.Application.TipoMateriales.Queries;
public class TipoMaterialDto : IMapFrom<TipoMaterial>
{
    public int? Id { get; set; }

    public string? Descripcion { get; set; }
}
