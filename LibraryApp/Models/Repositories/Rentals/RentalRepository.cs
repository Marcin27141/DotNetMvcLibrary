using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Renewals;
using Microsoft.EntityFrameworkCore;

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
            return _context.Rentals
                .Include(r => r.Reader)
                .Include(r => r.BookCopy)
                    .ThenInclude(bc => bc.Book)
                .Where(r => r.ReaderId.Equals(readerId)).ToList();
        }
    }
}
