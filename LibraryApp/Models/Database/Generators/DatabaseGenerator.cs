using LibraryApp.Models.Database.Generators.Books;
using LibraryApp.Models.Database.Generators.BooksCopies;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApp.Models.Database.Generators
{
    public class DatabaseGenerator(LibraryDbContext context) : IDatabaseGenerator
    {
        private readonly LibraryDbContext _context = context;

        public void SeedTables()
        {
            new BooksGenerator(_context).GenerateBooks();
            new BooksCopiesGenerator(_context).GenerateBooksCopies();
        }
    }
}
