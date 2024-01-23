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
    public class NameFormatVerifierTest
    {
        private NameFormatVerifier GetNameVerifier() => new NameFormatVerifier();

        [Theory]
        [InlineData("Marcin", true)]
        [InlineData("Adam", true)]
        [InlineData("", false)]
        [InlineData("marcin", false)]
        [InlineData("aDam", false)]
        [InlineData("MARCIN", false)]
        [InlineData("MarciN", false)]
        [InlineData("Pi0tr", false)]
        [InlineData("Adam#", false)]
        [InlineData("Adam Bartosz", false)]
        public void NameFormatVerifier_VerifyAccount_CorrectFormat(string name, bool expected)
        {
            //Arrange
            var validator = GetNameVerifier();
            var firstNameModel = new RegisterViewModel() { FirstName = name, LastName = name };

            //Act
            var firstNameCheck = validator.VerifyAccount(firstNameModel);

            //Assert
            firstNameCheck.IsSuccess.Should().Be(expected);
        }
    }
}
