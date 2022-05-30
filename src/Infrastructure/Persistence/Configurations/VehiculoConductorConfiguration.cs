using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class VehiculoConductorConfiguration : IEntityTypeConfiguration<VehiculoConductor>
{
    public void Configure(EntityTypeBuilder<VehiculoConductor> builder)
    {
        builder.HasKey(e => new { e.VehiculoId, e.ConductorId });

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
