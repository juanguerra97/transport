using seminario.Application.Common.Mappings;
using seminario.Domain.Entities;

namespace seminario.Application.PedidoMateriales.Queries;
public class EstadoPedidoMaterialDto : IMapFrom<EstadoPedidoMaterial>
{
    public int? Id { get; set; }
    public string? Descripcion { get; set; }
}
