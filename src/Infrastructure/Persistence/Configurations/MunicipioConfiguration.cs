
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class MunicipioConfiguration : IEntityTypeConfiguration<Municipio>
{
    public void Configure(EntityTypeBuilder<Municipio> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(m => m.Descripcion);

        builder.Property(m => m.Descripcion)
            .HasMaxLength(Municipio.MAX_DESCRIPCION_LENGTH)
            .IsRequired();

        builder.HasOne(m => m.Departamento)
            .WithMany()
            .HasForeignKey(m => m.DepartamentoId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

    }
}
