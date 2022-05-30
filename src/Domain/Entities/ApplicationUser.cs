using Microsoft.AspNetCore.Identity;

namespace seminario.Domain.Entities;
public class ApplicationUser : IdentityUser
{

    public static readonly int MAX_FIRSTNAME_LENGTH = 128;
    public static readonly int MAX_LASTNAME_LENGTH = 128;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}