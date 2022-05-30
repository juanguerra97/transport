
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class TipoEmpresaConfiguration : IEntityTypeConfiguration<TipoEmpresa>
{
    public void Configure(EntityTypeBuilder<TipoEmpresa> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Descripcion)
            .HasMaxLength(TipoEmpresa.MAX_DESCRIPCION_LENGTH)
            .IsRequired();
    }
}
