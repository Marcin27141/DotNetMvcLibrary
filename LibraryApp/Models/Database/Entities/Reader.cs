namespace LibraryApp.Models.Database.Entities
{
    public class Reader
    {
        public string LibraryUserId { get; set; }
        public LibraryUser LibraryUser { get; set; }
        public bool IsActive { get; set; }

        public IList<Rental> Rentals { get; set; }
        public IList<Penalty> Penalties { get; set; }

    }
}
