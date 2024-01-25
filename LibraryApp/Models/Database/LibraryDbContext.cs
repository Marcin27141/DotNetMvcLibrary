using LibraryApp.Models.Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System;
using Microsoft.Extensions.Hosting;

namespace LibraryApp.Models.Database
{
    public class LibraryDbContext : IdentityDbContext<LibraryUser>
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

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

            modelBuilder.Entity<Reader>()
            .HasOne(r => r.LibraryUser)
            .WithOne();

            modelBuilder.Entity<Payment>()
                .HasKey(p => p.PenaltyId);

            modelBuilder.Entity<Payment>()
            .HasOne(p => p.Penalty)
            .WithOne(p => p.Payment)
            .HasForeignKey<Payment>(p => p.PenaltyId);

            //modelBuilder.Entity<LibraryUser>()
            //    .HasQueryFilter(u => u.Status == "Active" && u.EmailConfirmed);
        }
    }

    
}
