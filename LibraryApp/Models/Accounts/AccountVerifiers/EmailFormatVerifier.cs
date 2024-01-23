using LibraryApp.Models.Accounts;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryApp.Models.Accounts.AccountVerifiers
{
    public class EmailFormatVerifier : IAccountVerifier
    {
        public AccountValidationResult VerifyAccount(RegisterViewModel user)
        {
            
            if (IsInCorrectFormat(user.Email))
                return AccountValidationResult.Success();
            else return AccountValidationResult.Failure(new AccountValidationError(
                nameof(user.Email), "Email must contain @"));
        }

        private bool IsInCorrectFormat(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.]+@[a-zA-Z]+\.[a-zA-Z]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
