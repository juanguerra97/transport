
using seminario.Application.Bodegas.Queries;
using seminario.Application.Common.Mappings;
using seminario.Application.Materiales.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.PedidoMateriales.Queries;
public class PedidoMaterialDto : IMapFrom<PedidoMaterial>
{
    public int? Id { get; set; }
    public EstadoPedidoMaterialDto? EstadoPedidoMaterial { get; set; }
    public BodegaDto? BodegaSolicita { get; set; }
    public string? Detalle { get; set; }
    public MaterialDto? Material { get; set; }
    public double? Cantidad { get; set; }
    public DateTime? FechaSolicitado { get; set; }
    public DateTime? FechaAprobado { get; set; }
    public DateTime? FechaCompletado { get; set; }
}
