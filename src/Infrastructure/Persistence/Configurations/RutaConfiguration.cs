using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class RutaConfiguration : IEntityTypeConfiguration<Ruta>
{
    public void Configure(EntityTypeBuilder<Ruta> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(e => e.TipoRuta)
            .WithMany()
            .HasForeignKey(e => e.TipoRutaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.UbicacionOrigen)
            .WithMany()
            .HasForeignKey(e => e.UbicacionOrigenId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.UbicacionDestino)
            .WithMany()
            .HasForeignKey(e => e.UbicacionDestinoId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

    }
}
