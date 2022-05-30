using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class EstadoTransporteCargaConfiguration : IEntityTypeConfiguration<EstadoTransporteCarga>
{
    public void Configure(EntityTypeBuilder<EstadoTransporteCarga> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Descripcion)
            .HasMaxLength(EstadoTransporteCarga.MAX_DESCRIPCION_LENGTH)
            .IsRequired();
    }
}
