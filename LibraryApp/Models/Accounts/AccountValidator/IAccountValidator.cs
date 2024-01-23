using LibraryApp.Models.Accounts;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Accounts.AccountValidator
{
    public interface IAccountValidator
    {
        AccountValidationResult Validate(RegisterViewModel user);
    }
}
