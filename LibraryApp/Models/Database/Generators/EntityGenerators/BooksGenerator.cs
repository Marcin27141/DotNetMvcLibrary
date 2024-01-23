using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.Database.Generators.EntityGenerators
{
    public class BooksGenerator(LibraryDbContext context)
    {
        private readonly LibraryDbContext _context = context;
        private readonly List<Book> books =
        [
            new Book
            {
                Title = "Hamlet",
                Author = "William Shakespeare",
                PublicationYear = 1600,
                Subject = "Theatrical drama about a man seeking revenge",
                OriginalLanguage = "English"
            },
            new Book
            {
                Title = "Romeo and Juliet",
                Author = "William Shakespeare",
                PublicationYear = 1597,
                Subject = "Tragic love story",
                OriginalLanguage = "English"
            },
            new Book
            {
                Title = "1984",
                Author = "George Orwell",
                PublicationYear = 1949,
                OriginalLanguage = "English"
            },
            new Book
            {
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                PublicationYear = 1960,
                Subject = "Southern Gothic novel",
                OriginalLanguage = "English"
            },
            new Book
            {
                Title = "Macbeth",
                Author = "William Shakespeare",
                PublicationYear = 1606,
                Subject = "Tragedy about the corrupting power of ambition",
                OriginalLanguage = "English"
            },
            new Book
            {
                Title = "Pride and Prejudice",
                Author = "Jane Austen",
                PublicationYear = 1813,
                Subject = "Classic novel exploring societal expectations and love",
                OriginalLanguage = "English"
            },
            new Book
            {
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                PublicationYear = 1925,
                Subject = "American novel depicting the Roaring Twenties",
                OriginalLanguage = "English"
            },
            new Book
            {
                Title = "One Hundred Years of Solitude",
                Author = "Gabriel Garcia Marquez",
                PublicationYear = 1967,
                OriginalLanguage = "Spanish"
            },
            new Book
            {
                Title = "The Hobbit",
                Author = "J.R.R. Tolkien",
                PublicationYear = 1937,
                Subject = "Fantasy novel about a hobbit's journey",
                OriginalLanguage = "English"
            },
            new Book
            {
                Title = "The Catcher in the Rye",
                Author = "J.D. Salinger",
                PublicationYear = 1951,
                Subject = "Coming-of-age novel",
                OriginalLanguage = "English"
            }
        ];

        public List<Book> GenerateBooks()
        {
            foreach (Book book in books)
            {
                _context.Books.Add(book);
            }
            _context.SaveChanges();
            return books;
        }
    }
}
