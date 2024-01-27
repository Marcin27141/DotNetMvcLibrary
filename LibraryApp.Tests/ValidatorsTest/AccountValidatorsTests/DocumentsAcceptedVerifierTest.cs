using FluentAssertions;
using LibraryApp.Models.Accounts.AccountVerifiers;
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
