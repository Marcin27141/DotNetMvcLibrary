using LibraryApp.Models.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Repositories.Accounts
{
    public interface IAccountRepository
    {
        IdentityResult ValidateUser(LibraryUser user);
        Task<string> GenerateEmailConfirmationTokenAsync(LibraryUser user);
        Task<IdentityResult> CreateUserAsync(LibraryUser user, string password);
        Task<IdentityResult> CreateReaderAsync(Reader reader, string password);
        Task SendConfirmationLinkAsync(LibraryUser user, string link);
        Task<Reader?> GetReaderByUsername(string username);
    }
}
