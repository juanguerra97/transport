using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Descripcion)
            .HasMaxLength(Material.MAX_DESCRIPCION_LENGTH)
            .IsRequired();

        builder.Property(e => e.Detalle)
            .HasMaxLength(Material.MAX_DETALLE_LENGTH);

        builder.HasOne(e => e.TipoMaterial)
            .WithMany()
            .HasForeignKey(e => e.TipoMaterialId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.UnidadMedida)
            .WithMany()
            .HasForeignKey(e => e.UnidadMedidaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
