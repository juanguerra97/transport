using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seminario.Domain.Entities;

namespace seminario.Infrastructure.Persistence.Configurations;
public class AdminEmpresaConfiguration : IEntityTypeConfiguration<AdminEmpresa>
{
    public void Configure(EntityTypeBuilder<AdminEmpresa> builder)
    {
        builder.HasKey(ae => new { ae.UserId, ae.EmpresaId });

        builder.HasOne(ae => ae.User)
            .WithMany()
            .HasForeignKey(ae => ae.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ae => ae.Empresa)
            .WithMany()
            .HasForeignKey(ae => ae.EmpresaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
            
    }
}
