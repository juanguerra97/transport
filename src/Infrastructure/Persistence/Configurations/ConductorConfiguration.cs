using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class ConductorConfiguration : IEntityTypeConfiguration<Conductor>
{
    public void Configure(EntityTypeBuilder<Conductor> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => e.UserId);

        builder.HasIndex(e => e.NoLicencia);

        builder.Property(e => e.NoLicencia)
            .HasMaxLength(Conductor.MAX_NOLICENCIA_LENGTH)
            .IsRequired();

        builder.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
