using LibraryApp.Models.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Repositories.Accounts
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(LibraryUser user, string password);
        Task<IdentityResult> CreateReaderAsync(Reader reader, string password);
        Task SendConfirmationLinkAsync(LibraryUser user);
        Task<Reader?> GetReaderByUsername(string username);
    }
}
