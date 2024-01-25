using FakeItEasy;
using LibraryApp.Models.Database;
using LibraryApp.Models.Repositories.Renewals.RenewalCreator;
using LibraryApp.Models.Repositories.Renewals.RenewalValidators;
using LibraryApp.Models.Repositories.Renewals;
using LibraryApp.Models.Specifications.RenewalSpecification;
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
        private const int TEST_RENTAL_ID = 100;
        public RenewalRepositoryTests()
        {
            _context = FakeDbContext.FakeDbContext.GetFakeDbContext();
            _renewalCreator = A.Fake<IRenewalCreator>();
            _renewalSpecification = A.Fake<IRenewalSpecification>();
            _renewalValidators = Enumerable.Empty<IRenewalValidator>();

            if (_context.Rentals.Find(TEST_RENTAL_ID) == null)
                AddTestRental();
        }

        private void AddTestRental()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            _context.Rentals.Add(new Rental()
            {
                RentalId = TEST_RENTAL_ID,
                RentalDate = today,
                OriginalReturnDeadline = today.AddDays(30),
                CurrentDeadline = today.AddDays(30),
                ReaderId = Guid.NewGuid().ToString(),
                BookCopyId = TEST_RENTAL_ID
            });
            _context.SaveChanges();
        }

        private RenewalRepository GetRenewalRepository() =>
            new RenewalRepository(_context, _renewalSpecification, _renewalCreator, _renewalValidators);

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(8)]
        public void RenewalRepository_GetRemainingRenewals_AlwaysInRange(int maxRenewals)
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

        [Theory]
        [InlineData(0, 2, 2)]
        [InlineData(1, 4, 3)]
        [InlineData(20, 41, 21)]
        public void RenewalRepository_GetRemainingRenewals_CorrectValue(int currentRenewals, int maxRenewals, int expected)
        {
            //Arrange
            A.CallTo(() => _renewalSpecification.MaxRenewalsPerRental).Returns(maxRenewals);
            var repo = GetRenewalRepository();
            var rental = new Rental() { Renewals = Enumerable.Repeat(new Renewal(), currentRenewals).ToList() };

            //Act
            int remaining = repo.GetRemainingRenewals(rental);

            //Assert
            remaining.Should().Be(expected);
        }

        [Fact]
        public void RenewalRepository_IsValidForRenewal_TrueWhenNoValidators()
        {
            //Arrange
            var repo = GetRenewalRepository();
            var rental = new Rental();

            //Act
            var validityCheck = repo.IsValidForRenewal(rental);

            //Assert
            validityCheck.IsValidForRenewal.Should().Be(true);
        }

        [Theory]
        [InlineData(30)]
        [InlineData(10)]
        [InlineData(1)]
        [InlineData(100)]
        public void RenewalRepository_RenewRental_CorrectlyRenewed(int renewalSpan)
        {
            //Arrange
            A.CallTo(() => _renewalSpecification.RenewalSpanInDays).Returns(renewalSpan);
            var repo = GetRenewalRepository();
            var rental = _context.Rentals.Find(TEST_RENTAL_ID);
            var previousDeadline = rental?.CurrentDeadline;
            A.CallTo(() => _renewalCreator.CreateRenewal(rental)).Returns(
                new Renewal() { NewReturnDeadline = previousDeadline.Value.AddDays(renewalSpan)}
                );

            //Act
            var validityCheck = repo.RenewRental(TEST_RENTAL_ID);

            //Assert
            var newDeadline = _context.Rentals.Find(TEST_RENTAL_ID)?.CurrentDeadline;
            newDeadline.Should().Be(previousDeadline.Value.AddDays(renewalSpan));
        }
    }
}
