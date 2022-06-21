using seminario.Domain.Entities;

namespace seminario.Application.Common.Constants;

public abstract class EstadosMovimientoBodegaConstants
{
    public static readonly EstadoMovimientoBodega PENDIENTE = new EstadoMovimientoBodega { Id = 1, Descripcion = "PENDIENTE" };
    public static readonly EstadoMovimientoBodega PROGRAMADO = new EstadoMovimientoBodega { Id = 2, Descripcion = "PROGRAMADO" };
    public static readonly EstadoMovimientoBodega CARGADO = new EstadoMovimientoBodega { Id = 3, Descripcion = "CARGADO" };
    public static readonly EstadoMovimientoBodega ENTREGADO = new EstadoMovimientoBodega { Id = 4, Descripcion = "ENTREGADO" };
    public static readonly EstadoMovimientoBodega ANULADO = new EstadoMovimientoBodega { Id = 5, Descripcion = "ANULADO" };
}
