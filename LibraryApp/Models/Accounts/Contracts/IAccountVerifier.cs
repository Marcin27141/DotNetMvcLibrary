using LibraryApp.Models.ViewModels;

namespace LibraryApp.Models.Accounts.Contracts
{
    public interface IAccountVerifier
    {
        AccountValidationResult VerifyAccount(RegisterViewModel user);
    }
}
