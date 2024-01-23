using FakeItEasy;
using FluentAssertions;
using LibraryApp.Models.Accounts.AccountVerifiers;
using LibraryApp.Models.ViewModels;
using LibraryApp.Models.Specifications.Passwords;

namespace LibraryApp.Tests.ValidatorsTest.AccountValidatorsTests
{
    public class PasswordStrengthVerifierTest
    {
        private IPasswordsSpecification _passwordSpecification;
        public PasswordStrengthVerifierTest()
        {
            _passwordSpecification = A.Fake<IPasswordsSpecification>();
        }
        private PasswordStrengthVerifier GetPasswordStrengthVerifier() => new PasswordStrengthVerifier(_passwordSpecification);

        [Theory]
        [InlineData("a", 1, true)]
        [InlineData("longPassword123", 10, true)]
        [InlineData("a", 5, false)]
        [InlineData("longPass", 10, false)]
        public void PasswordStrengthVerifier_VerifyAccount_LongEnough(string password, int minLength, bool expected)
        {
            //Arrange
            A.CallTo(() => _passwordSpecification.DigitsRequired).Returns(false);
            A.CallTo(() => _passwordSpecification.NonAlphanumericRequired).Returns(false);
            A.CallTo(() => _passwordSpecification.MinimumLength).Returns(minLength);
            var validator = GetPasswordStrengthVerifier();
            var userModel = new RegisterViewModel() { Password = password };

            //Act
            var validityCheck = validator.VerifyAccount(userModel);

            //Assert
            validityCheck.IsSuccess.Should().Be(expected);
        }

        [Theory]
        [InlineData("8", true)]
        [InlineData("9809834", true)]
        [InlineData("int8_t", true)]
        [InlineData("", false)]
        [InlineData("longButNoDigits!", false)]
        public void PasswordStrengthVerifier_VerifyAccount_WithDigitsRequired(string password, bool expected)
        {
            //Arrange
            A.CallTo(() => _passwordSpecification.DigitsRequired).Returns(true);
            A.CallTo(() => _passwordSpecification.NonAlphanumericRequired).Returns(false);
            A.CallTo(() => _passwordSpecification.MinimumLength).Returns(0);
            var validator = GetPasswordStrengthVerifier();
            var userModel = new RegisterViewModel() { Password = password };

            //Act
            var validityCheck = validator.VerifyAccount(userModel);

            //Assert
            validityCheck.IsSuccess.Should().Be(expected);
        }

        [Theory]
        [InlineData("#", true)]
        [InlineData("_._", true)]
        [InlineData("some()pass", true)]
        [InlineData("", false)]
        [InlineData("0specialInHere", false)]
        public void PasswordStrengthVerifier_VerifyAccount_WithSpecialSignsRequired(string password, bool expected)
        {
            //Arrange
            A.CallTo(() => _passwordSpecification.DigitsRequired).Returns(false);
            A.CallTo(() => _passwordSpecification.NonAlphanumericRequired).Returns(true);
            A.CallTo(() => _passwordSpecification.MinimumLength).Returns(0);
            var validator = GetPasswordStrengthVerifier();
            var userModel = new RegisterViewModel() { Password = password };

            //Act
            var validityCheck = validator.VerifyAccount(userModel);

            //Assert
            validityCheck.IsSuccess.Should().Be(expected);
        }

        [Theory]
        [InlineData("P@ssw0rd", 8, true)]
        [InlineData("_some100!!", 10, true)]
        [InlineData("so()23d*ic", 0, true)]
        [InlineData("", 0, false)]
        [InlineData("noDigitsButLong()", 8, false)]
        [InlineData("sh0Rt!", 8, false)]
        public void PasswordStrengthVerifier_VerifyAccount_AllRestrictions(string password, int minLength, bool expected)
        {
            //Arrange
            A.CallTo(() => _passwordSpecification.DigitsRequired).Returns(true);
            A.CallTo(() => _passwordSpecification.NonAlphanumericRequired).Returns(true);
            A.CallTo(() => _passwordSpecification.MinimumLength).Returns(minLength);
            var validator = GetPasswordStrengthVerifier();
            var userModel = new RegisterViewModel() { Password = password };

            //Act
            var validityCheck = validator.VerifyAccount(userModel);

            //Assert
            validityCheck.IsSuccess.Should().Be(expected);
        }
    }
}
