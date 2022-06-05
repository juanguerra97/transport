using seminario.Application.Common.Mappings;
using seminario.Domain.Entities;

namespace seminario.Application.TipoPlantas.Queries;
public class TipoPlantaDto : IMapFrom<TipoPlanta>
{
    public int? Id { get; set; }
    public string? Descripcion { get; set; }
}
