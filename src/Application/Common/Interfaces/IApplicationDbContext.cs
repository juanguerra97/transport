using seminario.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace seminario.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<ApplicationUser> ApplicationUsers { get; }

    DbSet<AdminBodega> AdminBodegas { get; }
    DbSet<AdminEmpresa> AdminEmpresas { get; }
    DbSet<AdminPlanta> AdminPlantas { get; }
    public DbSet<AlgoritmoMinimizacion> AlgoritmosMinimizacion { get; }
    public DbSet<BitacoraEstadoMovimientoBodega> BitacoraEstadoMovimientoBodegas { get; }
    public DbSet<BitacoraEstadoPedidoMaterial> BitacoraEstadoPedidoMateriales { get; }
    public DbSet<BitacoraEstadoSolicitudTransporteCarga> BitacoraEstadoSolicitudTransporteCargas { get; }
    public DbSet<BitacoraEstadoTransporteCarga> BitacoraEstadoTransporteCargas { get; }
    public DbSet<Bodega> Bodegas { get; }
    public DbSet<Conductor> Conductores { get; }
    public DbSet<Departamento> Departamentos { get; }
    public DbSet<DetalleTransporteCarga> DetalleTransporteCargas { get; }
    public DbSet<Empresa> Empresas { get; }
    public DbSet<EstadoMovimientoBodega> EstadosMovimientoBodega { get; }
    public DbSet<EstadoPedidoMaterial> EstadosPedidoMaterial { get; }
    public DbSet<EstadoSolicitudTransporteCarga> EstadosSolicitiudTransporteCarga { get; }
    public DbSet<EstadoTransporteCarga> EstadosTransporteCarga { get; }
    public DbSet<IngresoMaterial> IngresoMaterials { get; }
    public DbSet<InventarioBodega> InventarioBodegas { get; }
    public DbSet<Material> Materiales { get; }
    public DbSet<MovimientoBodega> MovimientoBodegas { get; }
    public DbSet<Municipio> Municipios { get; }
    public DbSet<Pais> Paises { get; }
    public DbSet<PedidoMaterial> PedidoMateriales { get; }
    public DbSet<Domain.Entities.Planta> Plantas { get;  }
    public DbSet<ProveedorMaterial> ProveedorMateriales { get; }
    public DbSet<Ruta> Rutas { get; }
    public DbSet<SolicitudTransporteCarga> SolicitudTransporteCargas { get; }
    public DbSet<TipoEmpresa> TipoEmpresas { get; }
    public DbSet<TipoMaterial> TipoMateriales { get; }
    public DbSet<TipoPlanta> TipoPlantas { get; }
    public DbSet<TipoRuta> TipoRutas { get; }
    public DbSet<Ubicacion> Ubicaciones { get; }
    public DbSet<UbicacionEmpresa> UbicacionEmpresas { get; }
    public DbSet<UnidadMedida> UnidadMedidas { get; }
    public DbSet<Vehiculo> Vehiculos { get; }
    public DbSet<VehiculoConductor> VehiculoConductores { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
