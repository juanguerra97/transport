
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class MovimientoBodegaConfiguration : IEntityTypeConfiguration<MovimientoBodega>
{
    public void Configure(EntityTypeBuilder<MovimientoBodega> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Detalle)
            .HasMaxLength(MovimientoBodega.MAX_DETALLE_LENGTH);

        builder.Property(e => e.Cantidad)
            .IsRequired();

        builder.HasOne(e => e.EstadoMovimientoBodega)
            .WithMany()
            .HasForeignKey(e => e.EstadoMovimientoBodegaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.PedidoMaterial)
            .WithMany()
            .HasForeignKey(e => e.PedidoMaterialId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.BodegaOrigen)
            .WithMany()
            .HasForeignKey(e => e.BodegaOrigenId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.BodegaDestino)
            .WithMany()
            .HasForeignKey(e => e.BodegaDestinoId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Material)
            .WithMany()
            .HasForeignKey(e => e.MaterialId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Vehiculo)
            .WithMany()
            .HasForeignKey(e => e.VehiculoId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Conductor)
            .WithMany()
            .HasForeignKey(e => e.ConductorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

    }
}
