
namespace seminario.Domain.Entities;
public class DetalleTransporteCarga : AuditableEntity
{
    public int? Id { get; set; }

    public int? SolicitudTransporteCargaId { get; set; }
    public SolicitudTransporteCarga? SolicitudTransporteCarga { get; set; }

    public int? EstadoTransporteCargaId { get; set; }
    public EstadoTransporteCarga? EstadoTransporteCarga { get; set; }

    public int? RutaId { get; set; }
    public Ruta? Ruta { get; set; }

    public DateTime? FechaInicioProgramado { get; set; }

    public DateTime? FechaFinProgramado { get; set; }

    public DateTime? FechaIniciado { get; set; }

    public DateTime? FechaTerminado { get; set; }

    public int? VehiculoId { get; set; }
    public Vehiculo? Vehiculo { get; set; }

    public int? ConductorId { get; set; }
    public Conductor? Conductor { get; set; }

}
