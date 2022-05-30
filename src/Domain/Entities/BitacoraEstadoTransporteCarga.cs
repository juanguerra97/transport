
namespace seminario.Domain.Entities;
public class BitacoraEstadoTransporteCarga : AuditableEntity
{
    public int? Id { get; set; }

    public int? DetalleTransporteCargaId { get; set; }
    public DetalleTransporteCarga? DetalleTransporteCarga { get; set; }

    public int? EstadoTransporteCargaId { get; set; }
    public EstadoTransporteCarga? EstadoTransporteCarga { get; set; }

}
