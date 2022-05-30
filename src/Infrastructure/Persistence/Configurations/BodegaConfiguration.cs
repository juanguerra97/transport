
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class BodegaConfiguration : IEntityTypeConfiguration<Bodega>
{
    public void Configure(EntityTypeBuilder<Bodega> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Descripcion)
            .HasMaxLength(Bodega.MAX_DESCRIPCION_LENGTH)
            .IsRequired();

        builder.Property(e => e.Detalle)
            .HasMaxLength(Bodega.MAX_DETALLE_LENGTH);

        builder.HasOne(e => e.Ubicacion)
            .WithMany()
            .HasForeignKey(e => e.UbicacionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    
    }
}
