using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Database.Generators.Books;
using LibraryApp.Models.Database.Generators.BooksCopies;
using LibraryApp.Models.Database.Generators.Rentals;
using LibraryApp.Models.Database.Generators.Roles;
using LibraryApp.Models.Database.Generators.Users;
using LibraryApp.Models.Repositories.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApp.Models.Database.Generators
{
    public class DatabaseGenerator(
        LibraryDbContext context,
        RoleManager<IdentityRole> roleManager,
        IAccountRepository accountRepository) : IDatabaseGenerator
    {
        private readonly LibraryDbContext _context = context;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IAccountRepository _accountRepository = accountRepository;

        public void SeedTables()
        {
            new RolesGenerator(_roleManager).GenerateRoles();
            new BooksGenerator(_context).GenerateBooks();
            new BooksCopiesGenerator(_context).GenerateBooksCopies();
            new UsersGenerator(_context, _accountRepository).GenerateUsers();
            new RentalsGenerator(_context).GenerateRentals();
        }
    }
}
