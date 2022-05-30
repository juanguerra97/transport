using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class InventarioBodegaConfiguration : IEntityTypeConfiguration<InventarioBodega>
{
    public void Configure(EntityTypeBuilder<InventarioBodega> builder)
    {
        builder.HasKey(e => new { e.BodegaId, e.MaterialId });

        builder.HasIndex(e => e.MaterialId);

        builder.Property(e => e.CantidadDisponible)
            .HasDefaultValue(0.0)
            .IsRequired();

        builder.Property(e => e.CantidadReservada)
            .HasDefaultValue(0.0)
            .IsRequired();

        builder.HasOne(e => e.Bodega)
            .WithMany()
            .HasForeignKey(e => e.BodegaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Material)
            .WithMany()
            .HasForeignKey(e => e.MaterialId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
