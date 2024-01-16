using LibraryApp.Models.Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Models.Database
{
    public class LibraryDbContext : IdentityDbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<LibraryUser> LibraryUsers { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Renewal> Renewals { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reader>()
                .HasKey(r => r.LibraryUserId);
        }
    }
}
