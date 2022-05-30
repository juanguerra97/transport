using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class PlantaConfiguration : IEntityTypeConfiguration<Planta>
{
    public void Configure(EntityTypeBuilder<Planta> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Descripcion)
            .HasMaxLength(Planta.MAX_DESCRIPCION_LENGTH)
            .IsRequired();

        builder.Property(e => e.Detalle)
            .HasMaxLength(Planta.MAX_DETALLE_LENGTH);

        builder.HasOne(e => e.TipoPlanta)
            .WithMany()
            .HasForeignKey(e => e.TipoPlantaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Bodega)
            .WithMany()
            .HasForeignKey(e => e.BodegaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}