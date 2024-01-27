using LibraryApp.Models.Accounts.Contracts;
using LibraryApp.Models.ViewModels;

namespace LibraryApp.Models.Accounts.AccountVerifiers
{
    public class PasswordEqualityVerifier : IAccountVerifier
    {
        public AccountValidationResult VerifyAccount(RegisterViewModel user)
        {
            return user.Password.Equals(user.ConfirmPassword) ?
                AccountValidationResult.Success() :
                AccountValidationResult.Failure(new AccountValidationError(
                nameof(user.ConfirmPassword), "Passwords must be the same"));
        }
    }
}
