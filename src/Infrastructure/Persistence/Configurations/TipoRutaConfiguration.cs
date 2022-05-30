using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class TipoRutaConfiguration : IEntityTypeConfiguration<TipoRuta>
{
    public void Configure(EntityTypeBuilder<TipoRuta> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Descripcion)
            .HasMaxLength(TipoRuta.MAX_DESCRIPCION_LENGTH)
            .IsRequired();

        builder.Property(t => t.Detalle)
            .HasMaxLength(TipoRuta.MAX_DETALLE_LENGTH)
            .IsRequired();
    }
}
