
namespace seminario.Domain.Entities;
public class BitacoraEstadoSolicitudTransporteCarga : AuditableEntity
{
    public int? Id { get; set; }

    public int? SolicitudTransporteCargaId { get; set; }
    public SolicitudTransporteCarga? SolicitudTransporteCarga { get; set; }  

    public int? EstadoSolicitudTransporteCargaId { get; set; }
    public EstadoSolicitudTransporteCarga? EstadoSolicitudTransporteCarga { get; set; }
}
