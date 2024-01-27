using LibraryApp.Models.Accounts.Contracts;
using LibraryApp.Models.Specifications.Passwords;
using LibraryApp.Models.ViewModels;

namespace LibraryApp.Models.Accounts.AccountVerifiers
{
    public class PasswordStrengthVerifier : IAccountVerifier
    {
        private readonly IPasswordsSpecification _passwordsSpecification;

        public PasswordStrengthVerifier(IPasswordsSpecification passwordsSpecification)
        {
            _passwordsSpecification = passwordsSpecification;
        }
        public AccountValidationResult VerifyAccount(RegisterViewModel user)
        {
            return IsPasswordValid(user.Password) ?
                AccountValidationResult.Success() :
                AccountValidationResult.Failure(new AccountValidationError(
                nameof(user.Password), GetPasswordRequirements()));
        }

        private bool IsPasswordValid(string password)
        {
            return password.Length >= _passwordsSpecification.MinimumLength &&
                (!_passwordsSpecification.DigitsRequired || password.Any(char.IsDigit)) &&
                (!_passwordsSpecification.NonAlphanumericRequired || password.Any(c => !char.IsLetterOrDigit(c)));
        }

        private string GetPasswordRequirements()
        {
            var mainInfo = $"Minimum length is {_passwordsSpecification.MinimumLength} characters";
            if (_passwordsSpecification.DigitsRequired)
                mainInfo += ", at least one digit";
            if (_passwordsSpecification.NonAlphanumericRequired)
                mainInfo += ", at least one special sign";
            return mainInfo;
        }
    }
}
