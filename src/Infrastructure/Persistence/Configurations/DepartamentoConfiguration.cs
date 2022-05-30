
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
{
    public void Configure(EntityTypeBuilder<Departamento> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(d => d.Descripcion);

        builder.Property(d => d.Descripcion)
            .HasMaxLength(Departamento.MAX_DESCRIPCION_LENGTH)
            .IsRequired();

        builder.HasOne(d => d.Pais)
            .WithMany()
            .HasForeignKey(d => d.PaisId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
