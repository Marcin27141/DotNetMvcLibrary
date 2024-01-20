using LibraryApp.Models.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Repositories.Accounts.AccountValidator
{
    public interface IAccountValidator
    {
        IdentityResult Validate(LibraryUser user);
    }
}
