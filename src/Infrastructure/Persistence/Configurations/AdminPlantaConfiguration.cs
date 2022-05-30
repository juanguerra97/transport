using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class AdminPlantaConfiguration : IEntityTypeConfiguration<AdminPlanta>
{
    public void Configure(EntityTypeBuilder<AdminPlanta> builder)
    {
        builder.HasKey(a => new { a.UserId, a.PlantaId });

        builder.HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Planta)
            .WithMany()
            .HasForeignKey(a => a.PlantaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
