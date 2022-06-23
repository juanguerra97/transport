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


    public DbSet<ApplicationUser> ApplicationUser => Set<ApplicationUser>();

    public DbSet<AdminBodega> AdminBodega => Set<AdminBodega>();
    public DbSet<AdminEmpresa> AdminEmpresa => Set<AdminEmpresa>();
    public DbSet<AdminPlanta> AdminPlanta => Set<AdminPlanta>();
    public DbSet<AlgoritmoMinimizacion> AlgoritmoMinimizacion => Set<AlgoritmoMinimizacion>();
    public DbSet<BitacoraEstadoMovimientoBodega> BitacoraEstadoMovimientoBodega => Set<BitacoraEstadoMovimientoBodega>();
    public DbSet<BitacoraEstadoPedidoMaterial> BitacoraEstadoPedidoMaterial => Set<BitacoraEstadoPedidoMaterial>();
    public DbSet<BitacoraEstadoSolicitudTransporteCarga> BitacoraEstadoSolicitudTransporteCarga => Set<BitacoraEstadoSolicitudTransporteCarga>();
    public DbSet<BitacoraEstadoTransporteCarga> BitacoraEstadoTransporteCarga => Set<BitacoraEstadoTransporteCarga>();
    public DbSet<Bodega> Bodega => Set<Bodega>();
    public DbSet<Conductor> Conductor => Set<Conductor>();
    public DbSet<Departamento> Departamento => Set<Departamento>();
    public DbSet<DetalleTransporteCarga> DetalleTransporteCarga => Set<DetalleTransporteCarga>();
    public DbSet<Empresa> Empresa => Set<Empresa>();
    public DbSet<EstadoMovimientoBodega> EstadoMovimientoBodega => Set<EstadoMovimientoBodega>();
    public DbSet<EstadoPedidoMaterial> EstadoPedidoMaterial => Set<EstadoPedidoMaterial>();
    public DbSet<EstadoSolicitudTransporteCarga> EstadoSolicitudTransporteCarga => Set<EstadoSolicitudTransporteCarga>();
    public DbSet<EstadoTransporteCarga> EstadoTransporteCarga => Set<EstadoTransporteCarga>();
    public DbSet<IngresoMaterial> IngresoMaterial => Set<IngresoMaterial>();
    public DbSet<InventarioBodega> InventarioBodega => Set<InventarioBodega>();
    public DbSet<Material> Material => Set<Material>();
    public DbSet<MovimientoBodega> MovimientoBodega => Set<MovimientoBodega>();
    public DbSet<Municipio> Municipio => Set<Municipio>();
    public DbSet<Pais> Pais => Set<Pais>();
    public DbSet<PedidoMaterial> PedidoMaterial => Set<PedidoMaterial>();
    public DbSet<Planta> Planta => Set<Planta>();
    public DbSet<ProveedorMaterial> ProveedorMaterial => Set<ProveedorMaterial>();
    public DbSet<Ruta> Ruta => Set<Ruta>();
    public DbSet<SolicitudTransporteCarga> SolicitudTransporteCarga => Set<SolicitudTransporteCarga>();
    public DbSet<TipoEmpresa> TipoEmpresa => Set<TipoEmpresa>();
    public DbSet<TipoMaterial> TipoMaterial => Set<TipoMaterial>();
    public DbSet<TipoPlanta> TipoPlanta => Set<TipoPlanta>();
    public DbSet<TipoRuta> TipoRuta => Set<TipoRuta>();
    public DbSet<Ubicacion> Ubicacion => Set<Ubicacion>();
    public DbSet<UbicacionEmpresa> UbicacionEmpresa => Set<UbicacionEmpresa>();
    public DbSet<UnidadMedida> UnidadMedida => Set<UnidadMedida>();
    public DbSet<Vehiculo> Vehiculo => Set<Vehiculo>();
    public DbSet<VehiculoConductor> VehiculoConductor => Set<VehiculoConductor>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Status = "A";
                    entry.Entity.UsuarioInsert = _currentUserService.UserId;
                    entry.Entity.FechaInsert = _dateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.UsuarioUpdate = _currentUserService.UserId;
                    entry.Entity.FechaUpdate = _dateTime.Now;
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
            userBuilder.Property<string>(u => u.FirstName)
                .HasMaxLength(Domain.Entities.ApplicationUser.MAX_FIRSTNAME_LENGTH)
                .IsRequired();

            userBuilder.Property<string>(u => u.LastName)
                .HasMaxLength(Domain.Entities.ApplicationUser.MAX_LASTNAME_LENGTH)
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
