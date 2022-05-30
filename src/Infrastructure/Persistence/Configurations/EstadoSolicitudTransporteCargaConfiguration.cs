using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class EstadoSolicitudTransporteCargaConfiguration : IEntityTypeConfiguration<EstadoSolicitudTransporteCarga>
{
    public void Configure(EntityTypeBuilder<EstadoSolicitudTransporteCarga> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Descripcion)
            .HasMaxLength(EstadoSolicitudTransporteCarga.MAX_DESCRIPCION_LENGTH)
            .IsRequired();
    }
}
