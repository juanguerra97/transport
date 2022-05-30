
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
{
    public void Configure(EntityTypeBuilder<Vehiculo> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .ValueGeneratedOnAdd();

        builder.Property(v => v.EsUsoInterno)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(v => v.Codigo)
            .HasMaxLength(Vehiculo.MAX_CODIGO_LENGTH);

        builder.Property(v => v.Placa)
            .HasMaxLength(Vehiculo.MAX_PLACA_LENGTH);

        builder.Property(v => v.Descripcion)
            .HasMaxLength(Vehiculo.MAX_DESCRIPCION_LENGTH)
            .IsRequired();

        builder.Property(v => v.Detalle)
            .HasMaxLength(Vehiculo.MAX_DETALLE_LENGTH)
            .IsRequired();

        builder.Property(v => v.CapacidadCarga)
            .IsRequired();

    }
}
