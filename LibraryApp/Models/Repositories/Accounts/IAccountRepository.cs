using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Repositories.Accounts
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(IdentityUser user, string password);
        Task SendConfirmationLinkAsync(IdentityUser user);
    }
}
