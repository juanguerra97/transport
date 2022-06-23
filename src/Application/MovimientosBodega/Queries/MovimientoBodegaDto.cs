using seminario.Application.Bodegas.Queries;
using seminario.Application.Common.Mappings;
using seminario.Application.Conductores.Queries;
using seminario.Application.Materiales.Queries;
using seminario.Application.PedidoMateriales.Queries;
using seminario.Application.Vehiculos.Queries;
using seminario.Domain.Entities;

namespace seminario.Application.MovimientosBodega.Queries;

public class MovimientoBodegaDto : IMapFrom<MovimientoBodega>
{
    public int? Id { get; set; }
    public EstadoMovimientoBodegaDto? EstadoMovimientoBodega { get; set; }
    public PedidoMaterialDto? PedidoMaterial { get; set; }
    public BodegaDto BodegaOrigen { get; set; }
    public BodegaDto? BodegaDestino { get; set; }
    public DateTime? FechaInicioProgramado { get; set; }
    public DateTime? FechaCargado { get; set; }
    public DateTime? FechaDescargado { get; set; }
    public double? Cantidad { get; set; }
    public string? Detalle { get; set; }
    public VehiculoDto? Vehiculo { get; set; }
    public ConductorDto? Conductor { get; set; }
}
