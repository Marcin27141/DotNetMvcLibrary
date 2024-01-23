using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.Database.Generators.EntityGenerators
{
    public class BooksCopiesGenerator(LibraryDbContext context)
    {
        private readonly LibraryDbContext _context = context;
        private const int COPIES_PER_BOOK = 5;

        public List<BookCopy> GenerateBooksCopies()
        {
            if (!_context.BookCopies.Any())
                SeedBooksCopies();
            return _context.BookCopies.ToList();
        }

        private void SeedBooksCopies()
        {
            foreach (Book book in _context.Books)
            {
                for (int i = 0; i < COPIES_PER_BOOK; i++)
                    _context.BookCopies.Add(new BookCopy
                    {
                        BookId = book.BookId
                    }); ;
            }
            _context.SaveChanges();
        }
    }
}
