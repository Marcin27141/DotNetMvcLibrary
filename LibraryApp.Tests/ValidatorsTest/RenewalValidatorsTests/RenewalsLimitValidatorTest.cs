using FakeItEasy;
using FluentAssertions;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Specifications.RenewalSpecification;
using LibraryApp.Models.Renewals.RenewalValidators;
using LibraryApp.Models.Renewals.RenewalErrors;

namespace LibraryApp.Tests.ValidatorsTest.RenewalValidatorsTests
{
    public class RenewalsLimitValidatorTest
    {
        private IRenewalSpecification _renewalSpecification;
        public RenewalsLimitValidatorTest()
        {
            _renewalSpecification = A.Fake<IRenewalSpecification>();
        }

        private RenewalLimitValidator GetRenewalLimitValidator() =>
            new RenewalLimitValidator(_renewalSpecification);

        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(3, 3, false)]
        [InlineData(0, 1, true)]
        [InlineData(3, 5, true)]
        public void RenewalLimitValidator_IsValidForRenewal_TestInOrOutOfRange(int currentRenewals, int maxRenewals, bool expected)
        {
            //Arrange
            A.CallTo(() => _renewalSpecification.MaxRenewalsPerRental).Returns(maxRenewals);
            var validator = GetRenewalLimitValidator();
            var rental = new Rental() { Renewals = Enumerable.Repeat(new Renewal(), currentRenewals).ToList() };

            //Act
            var validityCheck = validator.IsValidForRenewal(rental);

            //Assert
            validityCheck.IsSuccess.Should().Be(expected);
            if (validityCheck.Error != null)
            {
                var error = validityCheck.Error as RenewalsLimitReachedError;
                error?.Limit.Should().Be(maxRenewals);
            }
        }
    }
}
