using FakeItEasy;
using LibraryApp.Models.Database;
using LibraryApp.Models.Repositories.Renewals.RenewalCreator;
using LibraryApp.Models.Repositories.Renewals.RenewalValidators;
using LibraryApp.Models.Repositories.Renewals;
using LibraryApp.Models.Specifications.RenewalSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Models.Database.Entities;
using FluentAssertions;

namespace LibraryApp.Tests.RepositoriesTests
{
    public class RenewalRepositoryTests
    {
        private LibraryDbContext _context;
        private IRenewalCreator _renewalCreator;
        private IRenewalSpecification _renewalSpecification;
        private IEnumerable<IRenewalValidator> _renewalValidators;
        public RenewalRepositoryTests()
        {
            _context = FakeDbContext.FakeDbContext.GetFakeDbContext();
            _renewalCreator = A.Fake<IRenewalCreator>();
            _renewalSpecification = A.Fake<IRenewalSpecification>();
            _renewalValidators = Enumerable.Empty<IRenewalValidator>();
        }

        private RenewalRepository GetRenewalRepository() =>
            new RenewalRepository(_context, _renewalSpecification, _renewalCreator, _renewalValidators);

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public async Task RenewalRepository_GetRemainingRenewals_AlwaysInRange(int maxRenewals)
        {
            //Arrange
            A.CallTo(() => _renewalSpecification.MaxRenewalsPerRental).Returns(maxRenewals);
            var repo = GetRenewalRepository();
            var rental = new Rental() { Renewals = Enumerable.Repeat(new Renewal(), maxRenewals + 2).ToList() };
            var remainingList = new List<int>();

            //Act
            while (rental.Renewals.Any())
            {
                remainingList.Add(repo.GetRemainingRenewals(rental));
                rental.Renewals.RemoveAt(rental.Renewals.Count - 1);
            }

            //Assert
            remainingList.Should().AllSatisfy(x => Assert.InRange(x, 0, maxRenewals));
        }
    }
}
