using Azure.Core;
using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Accounts.AccountValidator;
using LibraryApp.Models.Repositories.EmailSender;
using LibraryApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Text.Encodings.Web;

namespace LibraryApp.Models.Repositories.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<LibraryUser> _userManager;
        private readonly IAccountValidator _accountValidator;
        private readonly ILibraryEmailSender _emailSender;
        private readonly LibraryDbContext _context;

        public int ActivationLinkValidityInHours => 48;

        public AccountRepository(
            UserManager<LibraryUser> userManager,
            IAccountValidator accountValidator,
            ILibraryEmailSender emailSender,
            LibraryDbContext context)
        {
            _userManager = userManager;
            _accountValidator = accountValidator;
            _emailSender = emailSender;
            _context = context;
        }

        public async Task<IdentityResult> CreateUserAsync(LibraryUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
                await _context.SaveChangesAsync();

            return result;
        }

        public async Task<IdentityResult> CreateReaderAsync(Reader reader, string password)
        {
            var result = await CreateUserAsync(reader.LibraryUser, password);
            if (result.Succeeded)
            {
                await _context.Readers.AddAsync(reader);
                await _userManager.AddToRoleAsync(reader.LibraryUser, "Reader");
                await _context.SaveChangesAsync();
            }
            
            return result;
        }

        public async Task<IdentityResult> AddClaimAsync(string userId, Claim claim)
        {
            var appUser = await _userManager.FindByIdAsync(userId);
            if (appUser == null)
                return IdentityResult.Failed();
            return await _userManager.AddClaimAsync(appUser, claim);
        }

        public async Task SendConfirmationLinkAsync(LibraryUser user, string link)
        {
            await _emailSender.SendConfirmationLinkAsync(user, link);
        }

        public async Task<Reader?> GetReaderByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null && await _userManager.IsInRoleAsync(user, "Reader"))
            {
                return _context.Readers.Find(user.Id);
            }
            return null;
        }

        public Task<string> GenerateEmailConfirmationTokenAsync(LibraryUser user)
        {
            return _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public AccountValidationResult ValidateUser(RegisterViewModel user) => _accountValidator.Validate(user);
    }
}
