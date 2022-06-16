using seminario.Application.Bodegas.Queries;
using seminario.Application.Common.Mappings;
using seminario.Application.Materiales.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.InventarioBodegas.Queries;

public class InventarioBodegaDto : IMapFrom<InventarioBodega>
{
    public BodegaDto? Bodega { get; set; }

    public MaterialDto? Material { get; set; }

    public double? CantidadDisponible { get; set; }

    public double? CantidadReservada { get; set; }
}
