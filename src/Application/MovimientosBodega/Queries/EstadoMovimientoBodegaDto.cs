using seminario.Application.Common.Mappings;
using seminario.Domain.Entities;

namespace seminario.Application.MovimientosBodega.Queries;

public class EstadoMovimientoBodegaDto : IMapFrom<EstadoMovimientoBodega>
{
    public int? Id { get; set; }
    public string? Descripcion { get; set; }
}
