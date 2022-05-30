
namespace seminario.Domain.Entities;
public class Ubicacion
{
    public static readonly int MAX_DESCRIPCION_LENGTH = 256;
    public static readonly int MAX_DETALLE_LENGTH = 1024;
    public static readonly int MAX_DIRECCION_LENGTH = 256;

    public int? Id { get; set; }

    public int? TipoUbicacionId { get; set; }
    public TipoUbicacion? TipoUbicacion { get; set; }

    public string? Descripcion { get; set; }

    public string? Detalle { get; set; }

    public int? MunicipioId { get; set; }
    public Municipio? Municipio { get; set; }

    public string? Direccion { get; set; }

    public double? Latitud { get; set; }

    public double? Longitud { get; set; }

}
