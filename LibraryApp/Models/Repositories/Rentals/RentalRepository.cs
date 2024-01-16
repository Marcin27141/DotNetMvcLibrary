using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Renewals;

namespace LibraryApp.Models.Repositories.Rentals
{
    public class RentalRepository : IRentalRepository
    {
        private readonly LibraryDbContext _context;

        public RentalRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public List<Rental> GetReaderRentals(string readerId)
        {
            return _context.Rentals.Where(r => r.ReaderId.Equals(readerId)).ToList();
        }
    }
}
