using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class BitacoraEstadoSolicitudTransporteCargaConfiguration : IEntityTypeConfiguration<BitacoraEstadoSolicitudTransporteCarga>
{
    public void Configure(EntityTypeBuilder<BitacoraEstadoSolicitudTransporteCarga> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => new { e.SolicitudTransporteCargaId, e.EstadoSolicitudTransporteCargaId });

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(e => e.SolicitudTransporteCarga)
            .WithMany()
            .HasForeignKey(e => e.SolicitudTransporteCargaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.EstadoSolicitudTransporteCarga)
            .WithMany()
            .HasForeignKey(e => e.EstadoSolicitudTransporteCargaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
