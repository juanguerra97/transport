using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class ProveedorMaterialConfiguration : IEntityTypeConfiguration<ProveedorMaterial>
{
    public void Configure(EntityTypeBuilder<ProveedorMaterial> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Nombre)
            .HasMaxLength(ProveedorMaterial.MAX_NOMBRE_LENGTH)
            .IsRequired();

        builder.Property(e => e.Nit)
            .HasMaxLength(ProveedorMaterial.MAX_NIT_LENGTH)
            .IsRequired();

        builder.Property(e => e.Telefono)
            .HasMaxLength(ProveedorMaterial.MAX_TELEFONO_LENGTH)
            .IsRequired();

        builder.Property(e => e.Email)
            .HasMaxLength(ProveedorMaterial.MAX_EMAIL_LENGTH)
            .IsRequired();

        builder.Property(e => e.Direccion)
            .HasMaxLength(ProveedorMaterial.MAX_DIRECCION_LENGTH)
            .IsRequired();
    }
}
