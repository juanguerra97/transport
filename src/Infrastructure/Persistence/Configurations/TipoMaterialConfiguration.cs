using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class TipoMaterialConfiguration : IEntityTypeConfiguration<TipoMaterial>
{
    public void Configure(EntityTypeBuilder<TipoMaterial> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.Descripcion)
            .HasMaxLength(TipoMaterial.MAX_DESCRIPCION_LENGTH)
            .IsRequired();
    }
}
