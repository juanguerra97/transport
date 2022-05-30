
namespace seminario.Domain.Entities;
public class SolicitudTransporteCarga
{
    public int? Id { get; set; }

    public int? EstadoSolicitudTransporteCargaId { get; set; }

    public EstadoSolicitudTransporteCarga? EstadoSolicitudTransporteCarga { get; set; }

    public int? AlgoritmoMinimizacionId { get; set; }

    public AlgoritmoMinimizacion? AlgoritmoMinimizacion { get; set; }

    public int? EmpresaId { get; set; }

    public Empresa? Empresa { get; set; }

    public int? UbicacionOrigenId { get; set; }
    public Ubicacion? UbicacionOrigen { get; set; }

    public int? UbicacionDestinoId { get; set; }
    public Ubicacion? UbicacionDestino { get; set; }

    public DateTime? FechaInicioSolicitado { get; set; }

    public DateTime? FechaAprobado { get; set; }

    public double? PesoCarga { get; set; }

    public decimal? Costo { get; set; }

    public int? TiempoEstimado { get; set; }

}
