using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Database.Generators.Roles
{
    public class RolesGenerator : IRolesGenerator
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesGenerator(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        } 

        public List<IdentityRole> GenerateRoles()
        {
            var roles = new List<IdentityRole>();
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new()
                {
                    Name = "Reader",
                };
                var addedSuccessfully = _roleManager.CreateAsync(role).Result;
                if (addedSuccessfully.Succeeded)
                    roles.Add(role);
            }
            return roles;
        }
    }
}
