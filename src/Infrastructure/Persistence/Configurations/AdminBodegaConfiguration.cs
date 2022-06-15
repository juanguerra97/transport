using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class AdminBodegaConfiguration : IEntityTypeConfiguration<AdminBodega>
{
    public void Configure(EntityTypeBuilder<AdminBodega> builder)
    {
        builder.HasKey(a => new { a.UserId, a.BodegaId });

        builder.HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Bodega)
            .WithOne(ad => ad.AdminBodega)
            .HasForeignKey((AdminBodega ad) => ad.BodegaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
