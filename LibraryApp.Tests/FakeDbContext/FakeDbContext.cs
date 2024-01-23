using FakeItEasy;
using FluentAssertions;
using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Renewals;
using LibraryApp.Models.Repositories.Renewals.RenewalCreator;
using LibraryApp.Models.Repositories.Renewals.RenewalValidators;
using LibraryApp.Models.Specifications.RenewalSpecification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Tests.FakeDbContext
{
    public class FakeDbContext
    {
        private static FakeDbContext? _fakeDbContext;
        public LibraryDbContext LibraryDbContext { get; set; }
        private Rental RentalWithTwoRenewals = new Rental()
        {
            Renewals = Enumerable.Repeat(new Renewal(), 2).ToList()
        };

        public static LibraryDbContext GetFakeDbContext()
        {
            _fakeDbContext ??= new FakeDbContext();
            return _fakeDbContext.LibraryDbContext;
        }
        private FakeDbContext()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContext = new LibraryDbContext(options);
            dbContext.Database.EnsureCreated();
            LibraryDbContext = dbContext;
        }
    }
}
