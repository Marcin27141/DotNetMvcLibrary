using FluentAssertions;
using LibraryApp.Models.Accounts.AccountVerifiers;
using LibraryApp.Models.ViewModels;

namespace LibraryApp.Tests.ValidatorsTest.AccountValidatorsTests
{
    public class ValidDateVerifierTest
    {
        private ValidDateVerifier GetValidDateVerifier() => new ValidDateVerifier();

        [Theory]
        [InlineData("01.01.2000", true)]
        [InlineData("31.12.2020", true)]
        [InlineData("01.01.2500", false)]
        [InlineData("01.01.100", false)]
        [InlineData("31.12.1700", false)]
        public void ValidDateVerifier_VerifyAccount_CheckIfValidDate(string date, bool expected)
        {
            //Arrange
            var validator = GetValidDateVerifier();
            var userModel = new RegisterViewModel() { Birthday = DateOnly.FromDateTime(DateTime.Parse(date))};

            //Act
            var validityCheck = validator.VerifyAccount(userModel);

            //Assert
            validityCheck.IsSuccess.Should().Be(expected);
        }
    }
}
