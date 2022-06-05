using seminario.Application.Common.Mappings;
using seminario.Application.Municipios.Queries;
using seminario.Domain.Entities;
using seminario.Domain.Enums;

namespace seminario.Application.Ubicaciones.Queries;
public class UbicacionDto : IMapFrom<Ubicacion>
{
    public int? Id { get; set; }
    public TipoUbicacion TipoUbicacion { get; set; }
    public string? Descripcion { get; set; }
    public string? Detalle { get; set; }
    public MunicipioDto? Municipio { get; set; }
    public string? Direccion { get; set; }
    public double? Latitud { get; set; }
    public double? Longitud { get; set; }
}
