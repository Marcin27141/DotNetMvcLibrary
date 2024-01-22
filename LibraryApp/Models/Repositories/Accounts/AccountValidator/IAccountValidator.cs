using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Repositories.Accounts.AccountValidator
{
    public interface IAccountValidator
    {
        AccountValidationResult Validate(RegisterViewModel user);
    }
}
