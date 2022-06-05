using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class TipoPlantaConfiguration : IEntityTypeConfiguration<TipoPlanta>
{
    public void Configure(EntityTypeBuilder<TipoPlanta> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.Descripcion)
            .HasMaxLength(TipoPlanta.MAX_DESCRIPCION_LENGTH)
            .IsRequired();
    }
}
