using LibraryApp.Models.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Repositories.Accounts
{
    public interface IAccountRepository
    {
        int ActivationLinkValidityInHours { get; }
        Task<string> GenerateEmailConfirmationTokenAsync(LibraryUser user);
        Task<IdentityResult> CreateUserAsync(LibraryUser user, string password);
        Task SendConfirmationLinkAsync(LibraryUser user, string link);
        Task AddToRoleAsync(LibraryUser user, string role);
        Task<LibraryUser?> GetUserInRole(string username, string role);
    }
}
