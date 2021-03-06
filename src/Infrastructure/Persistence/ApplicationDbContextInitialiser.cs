using seminario.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace seminario.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsMySql())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole("Administrator");

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        var adminPlantaRole = new IdentityRole("AdminPlanta");
        if (_roleManager.Roles.All(r => r.Name != adminPlantaRole.Name))
        {
            await _roleManager.CreateAsync(adminPlantaRole);
        }

        var adminBodegaRole = new IdentityRole("AdminBodega");
        if (_roleManager.Roles.All(r => r.Name != adminBodegaRole.Name))
        {
            await _roleManager.CreateAsync(adminBodegaRole);
        }

        var adminPedidosRole = new IdentityRole("AdminPedidos");
        if (_roleManager.Roles.All(r => r.Name != adminPedidosRole.Name))
        {
            await _roleManager.CreateAsync(adminPedidosRole);
        }

        var conductorRole = new IdentityRole("Conductor");
        if (_roleManager.Roles.All(r => r.Name != conductorRole.Name))
        {
            await _roleManager.CreateAsync(conductorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost", FirstName = "Admin", LastName = "Umg" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            await _userManager.AddToRolesAsync(administrator, new[] { adminPedidosRole.Name });
        }

    }
}