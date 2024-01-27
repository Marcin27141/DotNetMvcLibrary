using LibraryApp.Models.Accounts.Contracts;
using LibraryApp.Models.ViewModels;
using System.Text.RegularExpressions;

namespace LibraryApp.Models.Accounts.AccountVerifiers
{
    public class EmailFormatVerifier : IAccountVerifier
    {
        public AccountValidationResult VerifyAccount(RegisterViewModel user)
        {
            
            if (user.Email != null && IsInCorrectFormat(user.Email))
                return AccountValidationResult.Success();
            else return AccountValidationResult.Failure(new AccountValidationError(
                nameof(user.Email), "This is not a valid email"));
        }

        private bool IsInCorrectFormat(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.]+@[a-zA-Z]+\.[a-zA-Z]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
