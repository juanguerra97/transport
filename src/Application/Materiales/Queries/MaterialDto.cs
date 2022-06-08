using seminario.Application.Common.Mappings;
using seminario.Application.TipoMateriales.Queries;
using seminario.Application.UnidadesMedida.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Materiales.Queries;
public class MaterialDto : IMapFrom<Material>
{
    public int? Id { get; set; }

    public TipoMaterialDto? TipoMaterial { get; set; }

    public string? Descripcion { get; set; }

    public string? Detalle { get; set; }

    public UnidadMedidaDto UnidadMedida { get; set; }

    public double? Peso { get; set; }
}
