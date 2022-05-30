using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class AlgoritmoMinimizacionConfiguration : IEntityTypeConfiguration<AlgoritmoMinimizacion>
{
    public void Configure(EntityTypeBuilder<AlgoritmoMinimizacion> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedOnAdd();

        builder.Property(a => a.Descripcion)
            .HasMaxLength(AlgoritmoMinimizacion.MAX_DESCRIPCION_LENGTH)
            .IsRequired();
    }
}
