using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models
{
    public class RentalsViewModel(IEnumerable<Rental> rentals)
    {
        public IEnumerable<Rental> Rentals { get; set; } = rentals;
    }
}
