using seminario.Application.Common.Mappings;
using seminario.Application.Ubicaciones.Queries;
using seminario.Domain.Entities;
using seminario.Domain.Enums;

namespace seminario.Application.Bodegas.Queries;
public class BodegaDto : IMapFrom<Bodega>
{
    public int? Id { get; set; }
    public TipoBodega TipoBodega { get; set; }
    public string? Descripcion { get; set; }
    public string? Detalle { get; set; }
    public UbicacionDto? Ubicacion { get; set; }
}
