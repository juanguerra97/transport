using System.Reflection;
using seminario.Application.Common.Interfaces;
using seminario.Domain.Common;
using seminario.Domain.Entities;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace seminario.Infrastructure.Persistence;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;
    private readonly IDomainEventService _domainEventService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        ICurrentUserService currentUserService,
        IDomainEventService domainEventService,
        IDateTime dateTime) : base(options, operationalStoreOptions)
    {
        _currentUserService = currentUserService;
        _domainEventService = domainEventService;
        _dateTime = dateTime;
    }

    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();


    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();

    public DbSet<AdminBodega> AdminBodegas => Set<AdminBodega>();
    public DbSet<AdminEmpresa> AdminEmpresas => Set<AdminEmpresa>();
    public DbSet<AdminPlanta> AdminPlantas => Set<AdminPlanta>();
    public DbSet<AlgoritmoMinimizacion> AlgoritmosMinimizacion => Set<AlgoritmoMinimizacion>();
    public DbSet<BitacoraEstadoMovimientoBodega> BitacoraEstadoMovimientoBodegas => Set<BitacoraEstadoMovimientoBodega>();
    public DbSet<BitacoraEstadoPedidoMaterial> BitacoraEstadoPedidoMateriales => Set<BitacoraEstadoPedidoMaterial>();
    public DbSet<BitacoraEstadoSolicitudTransporteCarga> BitacoraEstadoSolicitudTransporteCargas => Set<BitacoraEstadoSolicitudTransporteCarga>();
    public DbSet<BitacoraEstadoTransporteCarga> BitacoraEstadoTransporteCargas => Set<BitacoraEstadoTransporteCarga>();
    public DbSet<Bodega> Bodegas => Set<Bodega>();
    public DbSet<Conductor> Conductores => Set<Conductor>();
    public DbSet<Departamento> Departamentos => Set<Departamento>();
    public DbSet<DetalleTransporteCarga> DetalleTransporteCargas => Set<DetalleTransporteCarga>();
    public DbSet<Empresa> Empresas => Set<Empresa>();
    public DbSet<EstadoMovimientoBodega> EstadosMovimientoBodega => Set<EstadoMovimientoBodega>();
    public DbSet<EstadoPedidoMaterial> EstadosPedidoMaterial => Set<EstadoPedidoMaterial>();
    public DbSet<EstadoSolicitudTransporteCarga> EstadosSolicitiudTransporteCarga => Set<EstadoSolicitudTransporteCarga>();
    public DbSet<EstadoTransporteCarga> EstadosTransporteCarga => Set<EstadoTransporteCarga>();
    public DbSet<IngresoMaterial> IngresoMaterials => Set<IngresoMaterial>();
    public DbSet<InventarioBodega> InventarioBodegas => Set<InventarioBodega>();
    public DbSet<Material> Materiales => Set<Material>();
    public DbSet<MovimientoBodega> MovimientoBodegas => Set<MovimientoBodega>();
    public DbSet<Municipio> Municipios => Set<Municipio>();
    public DbSet<Pais> Paises => Set<Pais>();
    public DbSet<PedidoMaterial> PedidoMateriales => Set<PedidoMaterial>();
    public DbSet<Planta> Plantas => Set<Planta>();
    public DbSet<ProveedorMaterial> ProveedorMateriales => Set<ProveedorMaterial>();
    public DbSet<Ruta> Rutas => Set<Ruta>();
    public DbSet<SolicitudTransporteCarga> SolicitudTransporteCargas => Set<SolicitudTransporteCarga>();
    public DbSet<TipoEmpresa> TipoEmpresas => Set<TipoEmpresa>();
    public DbSet<TipoMaterial> TipoMateriales => Set<TipoMaterial>();
    public DbSet<TipoPlanta> TipoPlantas => Set<TipoPlanta>();
    public DbSet<TipoRuta> TipoRutas => Set<TipoRuta>();
    public DbSet<TipoUbicacion> TipoUbicaciones => Set<TipoUbicacion>();
    public DbSet<Ubicacion> Ubicaciones => Set<Ubicacion>();
    public DbSet<UbicacionEmpresa> UbicacionEmpresas => Set<UbicacionEmpresa>();
    public DbSet<UnidadMedida> UnidadMedidas => Set<UnidadMedida>();
    public DbSet<Vehiculo> Vehiculos => Set<Vehiculo>();
    public DbSet<VehiculoConductor> VehiculoConductores => Set<VehiculoConductor>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Status = "A";
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    entry.Entity.Created = _dateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
            }
        }

        var events = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .ToArray();

        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents(events);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

       
        builder.Entity<ApplicationUser>(userBuilder =>
        {
            userBuilder.Property(u => u.FirstName)
                .HasMaxLength(ApplicationUser.MAX_FIRSTNAME_LENGTH)
                .IsRequired();

            userBuilder.Property(u => u.LastName)
                .HasMaxLength(ApplicationUser.MAX_LASTNAME_LENGTH)
                .IsRequired();

        });
    }

    private async Task DispatchEvents(DomainEvent[] events)
    {
        foreach (var @event in events)
        {
            @event.IsPublished = true;
            await _domainEventService.Publish(@event);
        }
    }
}
