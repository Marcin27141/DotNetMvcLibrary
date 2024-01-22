using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Repositories.Accounts
{
    public interface IAccountRepository
    {
        int ActivationLinkValidityInHours { get;  }
        AccountValidationResult ValidateUser(RegisterViewModel user);
        Task<string> GenerateEmailConfirmationTokenAsync(LibraryUser user);
        Task<IdentityResult> CreateUserAsync(LibraryUser user, string password);
        Task<IdentityResult> CreateReaderAsync(Reader reader, string password);
        Task SendConfirmationLinkAsync(LibraryUser user, string link);
        Task<Reader?> GetReaderByUsername(string username);
    }
}
