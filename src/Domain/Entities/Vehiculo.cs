
namespace seminario.Domain.Entities;
public class Vehiculo : AuditableEntity
{

    public static readonly int MAX_CODIGO_LENGTH = 128;
    public static readonly int MAX_PLACA_LENGTH = 64;
    public static readonly int MAX_DESCRIPCION_LENGTH = 256;
    public static readonly int MAX_DETALLE_LENGTH = 1024;

    public int? Id { get; set; }

    public bool? EsUsoInterno { get; set; } = false;

    public string? Codigo { get; set; }

    public string? Placa { get; set;  }

    public string? Descripcion { get; set; }

    public string? Detalle { get; set; }

    public double? CapacidadCarga { get; set; }

}
