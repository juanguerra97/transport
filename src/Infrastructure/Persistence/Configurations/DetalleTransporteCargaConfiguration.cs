using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class DetalleTransporteCargaConfiguration : IEntityTypeConfiguration<DetalleTransporteCarga>
{
    public void Configure(EntityTypeBuilder<DetalleTransporteCarga> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.FechaInicioProgramado)
            .IsRequired();

        builder.Property(e => e.FechaFinProgramado)
            .IsRequired();

        builder.HasOne(e => e.SolicitudTransporteCarga)
            .WithMany()
            .HasForeignKey(e => e.SolicitudTransporteCargaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.EstadoTransporteCarga)
            .WithMany()
            .HasForeignKey(e => e.EstadoTransporteCargaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Ruta)
            .WithMany()
            .HasForeignKey(e => e.RutaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Vehiculo)
            .WithMany()
            .HasForeignKey(e => e.VehiculoId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Conductor)
            .WithMany()
            .HasForeignKey(e => e.ConductorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
