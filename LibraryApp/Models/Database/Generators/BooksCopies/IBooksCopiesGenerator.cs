using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.Database.Generators.BooksCopies
{
    public interface IBooksCopiesGenerator
    {
        List<BookCopy> GenerateBooksCopies();
    }
}
