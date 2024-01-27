using FluentAssertions;
using LibraryApp.Models.Accounts.AccountVerifiers;
using LibraryApp.Models.ViewModels;

namespace LibraryApp.Tests.ValidatorsTest.AccountValidatorsTests
{
    public class EmailFormatVerifierTest
    {
        private EmailFormatVerifier GetEmailVerifier() => new EmailFormatVerifier();

        [Theory]
        [InlineData("some@op.pl", true)]
        [InlineData("1@u.p", true)]
        [InlineData("_12_some@io.oi", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("     ", false)]
        [InlineData("@", false)]
        [InlineData("@op.pl", false)]
        [InlineData("some@", false)]
        [InlineData("some.email.op.pl", false)]
        [InlineData("some#else@op.pl", false)]
        [InlineData("some.else@op.some.pl", false)]
        [InlineData("some@9.pl", false)]
        [InlineData("some email.op.pl", false)]
        public void EmailFormatVerifier_VerifyAccount_EmailInRightFormat(string? email, bool expected)
        {
            //Arrange
            var validator = GetEmailVerifier();
            var userModel = new RegisterViewModel() { Email = email };

            //Act
            var validityCheck = validator.VerifyAccount(userModel);

            //Assert
            validityCheck.IsSuccess.Should().Be(expected);
        }
    }
}
