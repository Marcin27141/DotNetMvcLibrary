using LibraryApp.Models.Accounts;
using LibraryApp.Models.Database.Generators.EntityGenerators;
using LibraryApp.Models.Repositories.Readers;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Database.Generators
{
    public class DatabaseGenerator(
        LibraryDbContext context,
        RoleManager<IdentityRole> roleManager,
        IReaderRepository readerRepository) : IDatabaseGenerator
    {
        private readonly LibraryDbContext _context = context;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IReaderRepository _readerRepository = readerRepository;

        public void SeedTables()
        {
            new RolesGenerator(_roleManager).GenerateRoles();
            new BooksGenerator(_context).GenerateBooks();
            new BooksCopiesGenerator(_context).GenerateBooksCopies();
            new ReadersGenerator(_context, _readerRepository).GenerateUsers();
            new RentalsGenerator(_context).GenerateRentals();
        }
    }
}
