using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.Database.Generators.Rentals
{
    public interface IRentalsGenerator
    {
        List<Rental> GenerateRentals();
    }
}
