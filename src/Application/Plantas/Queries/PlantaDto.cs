using seminario.Application.Bodegas.Queries;
using seminario.Application.Common.Mappings;
using seminario.Application.TipoPlantas.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.Plantas.Queries;
public class PlantaDto : IMapFrom<Planta>
{
    public int? Id { get; set; }
    public TipoPlantaDto? TipoPlanta { get; set; }
    public string? Descripcion { get; set; }
    public string? Detalle { get; set; }
    public BodegaDto? Bodega { get; set; }
}
