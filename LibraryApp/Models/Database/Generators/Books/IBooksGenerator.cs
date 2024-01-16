using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.Database.Generators.Books
{
    public interface IBooksGenerator
    {
        List<Book> GenerateBooks();
    }
}
