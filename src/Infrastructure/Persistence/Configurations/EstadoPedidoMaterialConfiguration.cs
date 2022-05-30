using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class EstadoPedidoMaterialConfiguration : IEntityTypeConfiguration<EstadoPedidoMaterial>
{
    public void Configure(EntityTypeBuilder<EstadoPedidoMaterial> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Descripcion)
            .HasMaxLength(EstadoPedidoMaterial.MAX_DESCRIPCION_LENGTH)
            .IsRequired();
    }
}
