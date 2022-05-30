using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class PedidoMaterialConfiguration : IEntityTypeConfiguration<PedidoMaterial>
{
    public void Configure(EntityTypeBuilder<PedidoMaterial> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Detalle)
            .HasMaxLength(PedidoMaterial.MAX_DETALLE_LENGTH);

        builder.Property(e => e.Cantidad)
            .IsRequired();

        builder.HasOne(e => e.EstadoPedidoMaterial)
            .WithMany()
            .HasForeignKey(e => e.EstadoPedidoMaterialId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.BodegaSolicita)
            .WithMany()
            .HasForeignKey(e => e.BodegaSolicitaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Material)
            .WithMany()
            .HasForeignKey(e => e.MaterialId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

    }
}
