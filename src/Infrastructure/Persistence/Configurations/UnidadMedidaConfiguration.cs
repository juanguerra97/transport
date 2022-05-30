
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class UnidadMedidaConfiguration : IEntityTypeConfiguration<UnidadMedida>
{
    public void Configure(EntityTypeBuilder<UnidadMedida> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();

        builder.Property(u => u.Descripcion)
            .HasMaxLength(UnidadMedida.MAX_DESCRIPCION_LENGTH)
            .IsRequired();

        builder.Property(u => u.DescripcionPlural)
            .HasMaxLength(UnidadMedida.MAX_DESCRIPCIONPLURAL_LENGTH)
            .IsRequired();

        builder.Property(u => u.DescripcionCorta)
            .HasMaxLength(UnidadMedida.MAX_DESCRIPCIONCORTA_LENGTH)
            .IsRequired();
    }
}
