using LibraryApp.Models.Accounts;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Accounts.AccountVerifiers
{
    public interface IAccountVerifier
    {
        AccountValidationResult VerifyAccount(RegisterViewModel user);
    }
}
