using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class BitacoraEstadoTransporteCargaConfiguration : IEntityTypeConfiguration<BitacoraEstadoTransporteCarga>
{
    public void Configure(EntityTypeBuilder<BitacoraEstadoTransporteCarga> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => new { e.DetalleTransporteCargaId, e.EstadoTransporteCargaId });

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(e => e.DetalleTransporteCarga)
            .WithMany()
            .HasForeignKey(e => e.DetalleTransporteCargaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.EstadoTransporteCarga)
            .WithMany()
            .HasForeignKey(e => e.EstadoTransporteCargaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
