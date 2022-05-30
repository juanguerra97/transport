using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class IngresoMaterialConfiguration : IEntityTypeConfiguration<IngresoMaterial>
{
    public void Configure(EntityTypeBuilder<IngresoMaterial> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(e => e.ProveedorMaterial)
            .WithMany()
            .HasForeignKey(e => e.ProveedorMaterialId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

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
