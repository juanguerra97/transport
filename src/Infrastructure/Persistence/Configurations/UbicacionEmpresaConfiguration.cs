using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class UbicacionEmpresaConfiguration : IEntityTypeConfiguration<UbicacionEmpresa>
{
    public void Configure(EntityTypeBuilder<UbicacionEmpresa> builder)
    {
        builder.HasKey(e => new { e.UbicacionId, e.EmpresaId });

        builder.HasOne(e => e.Ubicacion)
            .WithMany()
            .HasForeignKey(e => e.UbicacionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Empresa)
            .WithMany()
            .HasForeignKey(e => e.EmpresaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
