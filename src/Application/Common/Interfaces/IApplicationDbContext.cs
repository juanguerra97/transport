using seminario.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace seminario.Application.Common.Interfaces;

public interface IApplicationDbContext
{

    DbSet<ApplicationUser> ApplicationUser { get; }

    DbSet<AdminBodega> AdminBodega { get; }
    DbSet<AdminEmpresa> AdminEmpresa { get; }
    DbSet<AdminPlanta> AdminPlanta { get; }
    public DbSet<AlgoritmoMinimizacion> AlgoritmoMinimizacion { get; }
    public DbSet<BitacoraEstadoMovimientoBodega> BitacoraEstadoMovimientoBodega { get; }
    public DbSet<BitacoraEstadoPedidoMaterial> BitacoraEstadoPedidoMaterial { get; }
    public DbSet<BitacoraEstadoSolicitudTransporteCarga> BitacoraEstadoSolicitudTransporteCarga { get; }
    public DbSet<BitacoraEstadoTransporteCarga> BitacoraEstadoTransporteCarga { get; }
    public DbSet<Bodega> Bodega { get; }
    public DbSet<Conductor> Conductor { get; }
    public DbSet<Departamento> Departamento { get; }
    public DbSet<DetalleTransporteCarga> DetalleTransporteCarga { get; }
    public DbSet<Empresa> Empresa { get; }
    public DbSet<EstadoMovimientoBodega> EstadoMovimientoBodega { get; }
    public DbSet<EstadoPedidoMaterial> EstadoPedidoMaterial { get; }
    public DbSet<EstadoSolicitudTransporteCarga> EstadoSolicitudTransporteCarga { get; }
    public DbSet<EstadoTransporteCarga> EstadoTransporteCarga { get; }
    public DbSet<IngresoMaterial> IngresoMaterial { get; }
    public DbSet<InventarioBodega> InventarioBodega { get; }
    public DbSet<Material> Material { get; }
    public DbSet<MovimientoBodega> MovimientoBodega { get; }
    public DbSet<Municipio> Municipio { get; }
    public DbSet<Pais> Pais { get; }
    public DbSet<PedidoMaterial> PedidoMaterial { get; }
    public DbSet<Planta> Planta { get;  }
    public DbSet<ProveedorMaterial> ProveedorMaterial { get; }
    public DbSet<Ruta> Ruta { get; }
    public DbSet<SolicitudTransporteCarga> SolicitudTransporteCarga { get; }
    public DbSet<TipoEmpresa> TipoEmpresa { get; }
    public DbSet<TipoMaterial> TipoMaterial { get; }
    public DbSet<TipoPlanta> TipoPlanta { get; }
    public DbSet<TipoRuta> TipoRuta { get; }
    public DbSet<Ubicacion> Ubicacion { get; }
    public DbSet<UbicacionEmpresa> UbicacionEmpresa { get; }
    public DbSet<UnidadMedida> UnidadMedida { get; }
    public DbSet<Vehiculo> Vehiculo { get; }
    public DbSet<VehiculoConductor> VehiculoConductor { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
