using LibraryApp.Models.ViewModels;

namespace LibraryApp.Models.Accounts.Contracts
{
    public interface IAccountValidator
    {
        AccountValidationResult Validate(RegisterViewModel user);
    }
}
