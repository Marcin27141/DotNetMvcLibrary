using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.Database.Generators.Rentals
{
    public class RentalsGenerator(LibraryDbContext context) : IRentalsGenerator
    {
        private readonly LibraryDbContext _context = context;
        private const int RENTALS_PER_USER = 2;
        private const int HOW_MANY_USERS = 3;

        public List<Rental> GenerateRentals()
        {
            if (!_context.Rentals.Any())
                SeedRentals();
            return _context.Rentals.ToList();
        }

        private void SeedRentals()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var readers = _context.Readers.Take(HOW_MANY_USERS).ToList();
            var bookCopies = _context.BookCopies.ToList();

            foreach (var reader in readers)
            {
                for (int i = 0; i < RENTALS_PER_USER; i++)
                {
                    var deadline = today.AddDays(30);
                    _context.Rentals.Add(new Rental
                    {
                        RentalDate = today,
                        OriginalReturnDeadline = deadline,
                        CurrentDeadline = deadline,
                        ReaderId = reader.LibraryUserId,
                        BookCopyId = bookCopies[i].BookCopyId
                    });
                }
            }
            _context.SaveChanges();
        }
    }
}
