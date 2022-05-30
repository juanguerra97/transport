using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class TipoUbicacionConfiguration : IEntityTypeConfiguration<TipoUbicacion>
{
    public void Configure(EntityTypeBuilder<TipoUbicacion> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Descripcion)
            .HasMaxLength(TipoUbicacion.MAX_DESCRIPCION_LENGTH)
            .IsRequired();
    }
}
