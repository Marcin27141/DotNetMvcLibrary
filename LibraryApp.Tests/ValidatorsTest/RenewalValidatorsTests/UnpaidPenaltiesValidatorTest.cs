using FakeItEasy;
using FluentAssertions;
using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Renewals.RenewalCreator;
using LibraryApp.Models.Repositories.Renewals.RenewalValidators;
using LibraryApp.Models.Repositories.Renewals;
using LibraryApp.Models.Specifications.RenewalSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Tests.ValidatorsTest.RenewalValidatorsTests
{
    public class UnpaidPenaltiesValidatorTest
    {
        private IRenewalSpecification _renewalSpecification;
        public UnpaidPenaltiesValidatorTest()
        {
            _renewalSpecification = A.Fake<IRenewalSpecification>();
        }

        private UnpaidPenaltiesValidator GetUnpaidPenaltiesValidator() =>
            new UnpaidPenaltiesValidator(_renewalSpecification);

        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(1, 5, true)]
        [InlineData(3, 3, true)]
        [InlineData(1, 0, false)]
        [InlineData(4, 1, false)]
        [InlineData(3, 2, false)]
        public void UnpaidPenaltiesValidator_IsValidForRenewal_TestInOrOutOfRange(int currentPenalties, int maxPenalties, bool expected)
        {
            //Arrange
            A.CallTo(() => _renewalSpecification.AllowedPenalties).Returns(maxPenalties);
            var validator = GetUnpaidPenaltiesValidator();
            var reader = new Reader()
            {
                Penalties = Enumerable.Repeat(new Penalty(), currentPenalties).ToList()
            };
            var rental = new Rental() { Reader = reader };

            //Act
            var validityCheck = validator.IsValidForRenewal(rental);

            //Assert
            validityCheck.IsSuccess.Should().Be(expected);
            if (validityCheck.Error != null)
            {
                var error = validityCheck.Error as HasUnpaidPenaltiesError;
                error?.NumberOfUnpaidPenalties.Should().Be(currentPenalties);
            }
        }
    }
}
