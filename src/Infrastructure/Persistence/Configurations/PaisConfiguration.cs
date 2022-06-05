using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class PaisConfiguration : IEntityTypeConfiguration<Pais>
{
    public void Configure(EntityTypeBuilder<Pais> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.HasIndex(p => p.Descripcion).IsUnique();

        builder.Property(p => p.Descripcion)
            .HasMaxLength(Pais.MAX_DESCRIPCION_LENGTH)
            .IsRequired();

    }
}
