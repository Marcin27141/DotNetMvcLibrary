using LibraryApp.Models.Accounts.Contracts;
using LibraryApp.Models.ViewModels;

namespace LibraryApp.Models.Accounts.AccountVerifiers
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
