using seminario.Domain.Entities;

namespace seminario.Application.Common.Constants;
public abstract class EstadosPedidoMaterialConstants
{
    public static readonly EstadoPedidoMaterial CREADO = new EstadoPedidoMaterial { Id = 1, Descripcion = "CREADO" };
    public static readonly EstadoPedidoMaterial PENDIENTE = new EstadoPedidoMaterial { Id = 2, Descripcion = "PENDIENTE" };
    public static readonly EstadoPedidoMaterial APROBADO = new EstadoPedidoMaterial { Id = 3, Descripcion = "APROBADO" };
    public static readonly EstadoPedidoMaterial PROGRAMADO = new EstadoPedidoMaterial { Id = 4, Descripcion = "PROGRAMADO" };
    public static readonly EstadoPedidoMaterial COMPLETADO = new EstadoPedidoMaterial { Id = 5, Descripcion = "COMPLETADO" };
    public static readonly EstadoPedidoMaterial ANULADO = new EstadoPedidoMaterial { Id = 6, Descripcion = "ANULADO" };
}
