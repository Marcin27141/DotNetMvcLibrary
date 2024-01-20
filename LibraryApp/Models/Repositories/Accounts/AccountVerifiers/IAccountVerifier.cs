using LibraryApp.Models.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Repositories.Accounts.AccountValidator
{
    public interface IAccountVerifier
    {
        IdentityResult VerifyAccount(LibraryUser user);
    }
}
