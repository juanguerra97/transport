
namespace seminario.Domain.Entities;
public class BitacoraEstadoMovimientoBodega : AuditableEntity
{

    public int? Id { get; set; }
    public int? MovimientoBodegaId { get; set; }
    public MovimientoBodega? MovimientoBodega { get; set; }

    public int? EstadoMovimientoBodegaId { get; set; }
    public EstadoMovimientoBodega? EstadoMovimientoBodega { get; set; }
}
