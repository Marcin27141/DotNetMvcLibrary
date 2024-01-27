using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.Repositories.Rentals
{
    public interface IRentalRepository
    {
        Rental? GetRentalById(int id);
        List<Rental> GetReaderRentals(string readerId);
    }
}
