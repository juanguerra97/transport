using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class BitacoraEstadoMovimientoBodegaConfiguration : IEntityTypeConfiguration<BitacoraEstadoMovimientoBodega>
{
    public void Configure(EntityTypeBuilder<BitacoraEstadoMovimientoBodega> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.HasIndex(e => new { e.MovimientoBodegaId, e.EstadoMovimientoBodegaId });

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(e => e.MovimientoBodega)
            .WithMany()
            .HasForeignKey(e => e.MovimientoBodegaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.EstadoMovimientoBodega)
            .WithMany()
            .HasForeignKey(e => e.EstadoMovimientoBodegaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
