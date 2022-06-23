
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;

public class EstadoMovimientoBodegaConfiguration : IEntityTypeConfiguration<EstadoMovimientoBodega>
{
    public void Configure(EntityTypeBuilder<EstadoMovimientoBodega> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.Descripcion)
            .HasMaxLength(EstadoMovimientoBodega.MAX_DESCRIPCION_LENGTH)
            .IsRequired();

    }
}
