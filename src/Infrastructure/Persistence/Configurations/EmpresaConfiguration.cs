using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Descripcion)
            .HasMaxLength(Empresa.MAX_DESCRIPCION_LENGTH)
            .IsRequired();

        builder.Property(e => e.Detalle)
            .HasMaxLength(Empresa.MAX_DETALLE_LENGTH);

        builder.Property(e => e.Nit)
            .HasMaxLength(Empresa.MAX_NIT_LENGTH);

        builder.HasOne(e => e.TipoEmpresa)
            .WithMany()
            .HasForeignKey(e => e.TipoEmpresaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
