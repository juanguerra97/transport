
namespace seminario.Domain.Entities;
public class Ruta : AuditableEntity
{
    public int? Id { get; set; }

    public int? TipoRutaId { get; set; }
    public TipoRuta? TipoRuta { get; set; }

    public int? UbicacionOrigenId { get; set; }
    public Ubicacion? UbicacionOrigen { get; set; }

    public int? UbicacionDestinoId { get; set; }
    public Ubicacion? UbicacionDestino { get; set; }

    public double? Distancia { get; set; }

    public int? Tiempo { get; set; }

    public decimal? CostoPorCarga { get; set; }

}
