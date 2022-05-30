
namespace seminario.Domain.Entities;
public class BitacoraEstadoPedidoMaterial : AuditableEntity
{
    public int? Id { get; set; }

    public int? PedidoMaterialId { get; set; }
    public PedidoMaterial? PedidoMaterial { get; set; }

    public int? EstadoPedidoMaterialId { get; set; }
    public EstadoPedidoMaterial? EstadoPedidoMaterial { get; set; }
}
