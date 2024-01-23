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
    public class PasswordEqualityVerifierTest
    {
        private PasswordEqualityVerifier GetPasswordEqualityVerifier() => new PasswordEqualityVerifier();

        [Theory]
        [InlineData("", "", true)]
        [InlineData("a", "a", true)]
        [InlineData("some#pass09()", "some#pass09()", true)]
        [InlineData("", "a", false)]
        [InlineData("some", " some ", false)]
        [InlineData("j89", "j890", false)]
        public void PasswordEqualityVerifier_VerifyAccount_PasswordsAreEqual(string password, string confirm, bool expected)
        {
            //Arrange
            var validator = GetPasswordEqualityVerifier();
            var userModel = new RegisterViewModel() { Password = password, ConfirmPassword = confirm };

            //Act
            var validityCheck = validator.VerifyAccount(userModel);

            //Assert
            validityCheck.IsSuccess.Should().Be(expected);
        }
    }
}
