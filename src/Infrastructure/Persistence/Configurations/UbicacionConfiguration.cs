using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class UbicacionConfiguration : IEntityTypeConfiguration<Ubicacion>
{
    public void Configure(EntityTypeBuilder<Ubicacion> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Descripcion)
            .HasMaxLength(Ubicacion.MAX_DESCRIPCION_LENGTH)
            .IsRequired();

        builder.Property(e => e.Detalle)
            .HasMaxLength(Ubicacion.MAX_DETALLE_LENGTH);

        builder.Property(e => e.Direccion)
            .HasMaxLength(Ubicacion.MAX_DIRECCION_LENGTH)
            .IsRequired();

        builder.HasOne(e => e.TipoUbicacion)
            .WithMany()
            .HasForeignKey(e => e.TipoUbicacionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Municipio)
            .WithMany()
            .HasForeignKey(e => e.MunicipioId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
