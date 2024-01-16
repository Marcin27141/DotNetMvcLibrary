namespace LibraryApp.Models.Database.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string? Author { get; set; }
        public int? PublicationYear { get; set; }
        public string? Subject { get; set; }
        public string? OriginalLanguage { get; set; }
    }
}
