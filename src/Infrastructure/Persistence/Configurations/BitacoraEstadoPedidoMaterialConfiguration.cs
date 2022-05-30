using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class BitacoraEstadoPedidoMaterialConfiguration : IEntityTypeConfiguration<BitacoraEstadoPedidoMaterial>
{
    public void Configure(EntityTypeBuilder<BitacoraEstadoPedidoMaterial> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => new { e.PedidoMaterialId, e.EstadoPedidoMaterialId });

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(e => e.PedidoMaterial)
            .WithMany()
            .HasForeignKey(e => e.PedidoMaterialId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.EstadoPedidoMaterial)
            .WithMany()
            .HasForeignKey(e => e.EstadoPedidoMaterialId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
