using LibraryApp.Models.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Database.Generators.Roles
{
    public interface IRolesGenerator
    {
        List<IdentityRole> GenerateRoles();
    }
}
