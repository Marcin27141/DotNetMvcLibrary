namespace LibraryApp.Models.Database.Entities
{
    public class BookCopy
    {
        public int BookCopyId { get; set; }
        public string? State { get; set; }
        public string? Language { get; set; }

        //relations
        public Book Book { get; set; }
        public int BookId { get; set; }

    }
}
