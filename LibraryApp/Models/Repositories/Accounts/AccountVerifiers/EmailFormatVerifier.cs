using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Accounts.AccountValidator;
using LibraryApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryApp.Models.Repositories.Accounts.AccountVerifiers
{
    public class EmailFormatVerifier : IAccountVerifier
    {
        public AccountValidationResult VerifyAccount(RegisterViewModel user)
        {
            if (user.Email.IsNullOrEmpty() || user.Email.Contains('@'))
                return AccountValidationResult.Success();
            else return AccountValidationResult.Failure(new AccountValidationError(
                nameof(user.Email), "Email must contain @"));
        }
    }
}
