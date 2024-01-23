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
using LibraryApp.Models.Accounts.AccountVerifiers;
using System.Xml.Serialization;
using LibraryApp.Models.ViewModels;

namespace LibraryApp.Tests.ValidatorsTest.AccountValidatorsTests
{
    public class UserAgeVerifierTest
    {
        private UserAgeVerifier GetUserAgeVerifier() => new UserAgeVerifier();

        [Theory]
        [InlineData("01.01.2000", true)]
        [InlineData("31.12.1492", true)]
        [InlineData("23.09.2004", true)]
        [InlineData("01.01.2015", false)]
        [InlineData("01.01.2030", false)]
        public void UserAgeVerifier_VerifyAccount_CheckIfTooYoung(string birthday, bool expected)
        {
            //Arrange
            var validator = GetUserAgeVerifier();
            var userModel = new RegisterViewModel() { Birthday = DateOnly.FromDateTime(DateTime.Parse(birthday))};

            //Act
            var validityCheck = validator.VerifyAccount(userModel);

            //Assert
            validityCheck.IsSuccess.Should().Be(expected);
        }
    }
}
