namespace LibraryApp.Models.Database.Entities
{
    public class Rental
    {
        public int RentalId { get; set; }
        public DateOnly RentalDate { get; set; }
        public DateOnly OriginalReturnDeadline { get; set; }
        public DateOnly? ReturnDate { get; set; }
        

        //relations
        public int ReaderId { get; set; }
        public Reader Reader { get; set; }

        public int BookCopyId { get; set; }
        public BookCopy BookCopy { get; set; }
    }
}
