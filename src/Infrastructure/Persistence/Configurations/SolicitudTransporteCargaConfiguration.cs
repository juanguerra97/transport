using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class SolicitudTransporteCargaConfiguration : IEntityTypeConfiguration<SolicitudTransporteCarga>
{
    public void Configure(EntityTypeBuilder<SolicitudTransporteCarga> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.PesoCarga)
            .IsRequired();

        builder.HasOne(e => e.EstadoSolicitudTransporteCarga)
            .WithMany()
            .HasForeignKey(e => e.EstadoSolicitudTransporteCargaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.AlgoritmoMinimizacion)
            .WithMany()
            .HasForeignKey(e => e.AlgoritmoMinimizacionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Empresa)
            .WithMany()
            .HasForeignKey(e => e.EmpresaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.UbicacionOrigen)
            .WithMany()
            .HasForeignKey(e => e.UbicacionOrigenId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.UbicacionDestino)
            .WithMany()
            .HasForeignKey(e => e.UbicacionDestinoId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

    }
}
