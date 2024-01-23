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
    public class DocumentsAcceptedVerifierTest
    {
        private DocumentsAcceptedVerifier GetDocumentsAcceptedVerifier() => new DocumentsAcceptedVerifier();

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void DocumentsAcceptedVerifier_VerifyAccount_DocumentsAccepted(bool documentsAccepted, bool expected)
        {
            //Arrange
            var validator = GetDocumentsAcceptedVerifier();
            var userModel = new RegisterViewModel() { TermsAccepted = documentsAccepted };

            //Act
            var validityCheck = validator.VerifyAccount(userModel);

            //Assert
            validityCheck.IsSuccess.Should().Be(expected);
        }
    }
}
