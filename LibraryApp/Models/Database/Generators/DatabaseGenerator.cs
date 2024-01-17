using LibraryApp.Models.Database.Generators.Books;
using LibraryApp.Models.Database.Generators.BooksCopies;
using LibraryApp.Models.Database.Generators.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApp.Models.Database.Generators
{
    public class DatabaseGenerator(LibraryDbContext context, RoleManager<IdentityRole> roleManager) : IDatabaseGenerator
    {
        private readonly LibraryDbContext _context = context;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public void SeedTables()
        {
            new RolesGenerator(_roleManager).GenerateRoles();
            new BooksGenerator(_context).GenerateBooks();
            new BooksCopiesGenerator(_context).GenerateBooksCopies();
        }
    }
}
