using seminario.Application.Common.Mappings;
using seminario.Domain.Entities;

namespace seminario.Application.Vehiculos.Queries;
public class VehiculoDto : IMapFrom<Vehiculo>
{
    public int? Id { get; set; }
    public bool? EsUsoInterno { get; set; } = false;
    public string? Codigo { get; set; }
    public string? Placa { get; set; }
    public string? Descripcion { get; set; }
    public string? Detalle { get; set; }
    public double? CapacidadCarga { get; set; }
    public string? Status { get; set; }
}
