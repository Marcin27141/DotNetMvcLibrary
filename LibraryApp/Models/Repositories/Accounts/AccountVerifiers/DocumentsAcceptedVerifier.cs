using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Accounts.AccountValidator;
using LibraryApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryApp.Models.Repositories.Accounts.AccountVerifiers
{
    public class DocumentsAcceptedVerifier : IAccountVerifier
    {
        public AccountValidationResult VerifyAccount(RegisterViewModel user)
        {
            return user.TermsAccepted ?
                AccountValidationResult.Success() :
                AccountValidationResult.Failure(new AccountValidationError(
                    nameof(user.TermsAccepted), "You must accepts these documents to register."));
        }
    }
}
