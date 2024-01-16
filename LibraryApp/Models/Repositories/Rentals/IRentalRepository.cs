using LibraryApp.Models.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Repositories.Rentals
{
    public interface IRentalRepository
    {
        List<Rental> GetReaderRentals(string readerId);
    }
}
