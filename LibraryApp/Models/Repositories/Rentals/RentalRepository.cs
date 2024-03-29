﻿using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
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
                .Include(r => r.BookCopy)
                    .ThenInclude(bc => bc.Book)
                .Where(r => r.ReaderId.Equals(readerId)).ToList();
        }

        public Rental? GetRentalById(int id)
        {
            return _context.Rentals
                .Include(r => r.Reader)
                    .ThenInclude(r => r.Penalties)
                        .ThenInclude(p => p.Payment)
                .Include(r => r.Reader)
                .Include(r => r.BookCopy)
                    .ThenInclude(bc => bc.Book)
                .Include(r => r.Renewals)
                .FirstOrDefault(r => r.RentalId == id);
        }
    }
}
