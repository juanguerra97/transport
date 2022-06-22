using seminario.Application.Common.Interfaces;
using seminario.Infrastructure.Identity;
using seminario.Infrastructure.Persistence;
using seminario.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using seminario.Domain.Entities;
using System.Security.Claims;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;

namespace seminario.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("seminarioDb"));
        }
        else
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped<IDomainEventService, DomainEventService>();

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>()
            .AddProfileService<CustomProfileService>();

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();

        services.AddAuthentication()
            .AddIdentityServerJwt();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminCatalogo", policy => policy.RequireAssertion(context =>
            {
                return context.User.HasClaim(c => (c.Type == ClaimTypes.Role || c.Type == JwtClaimTypes.Role) && c.Value == "Administrator");
            }));
            options.AddPolicy("AdminBodega", policy => policy.RequireAssertion(context =>
            {
                return context.User.HasClaim(c => (c.Type == ClaimTypes.Role || c.Type == JwtClaimTypes.Role) && c.Value == "AdminBodega");
            }));
            options.AddPolicy("AdminPedidos", policy => policy.RequireAssertion(context =>
            {
                return context.User.HasClaim(c => (c.Type == ClaimTypes.Role || c.Type == JwtClaimTypes.Role) && c.Value == "AdminPedidos");
            }));
            options.AddPolicy("Conductor", policy => policy.RequireAssertion(context =>
            {
                return context.User.HasClaim(c => (c.Type == ClaimTypes.Role || c.Type == JwtClaimTypes.Role) && c.Value == "Conductor");
            }));
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
        });

        return services;
    }
}
