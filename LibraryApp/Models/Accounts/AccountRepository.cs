using Azure.Core;
using LibraryApp.Models.Accounts.AccountValidator;
using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
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

namespace LibraryApp.Models.Accounts
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

        public async Task SendConfirmationLinkAsync(LibraryUser user, string link)
        {
            await _emailSender.SendConfirmationLinkAsync(user, link);
        }

        public Task<string> GenerateEmailConfirmationTokenAsync(LibraryUser user)
        {
            return _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public AccountValidationResult ValidateUser(RegisterViewModel user) => _accountValidator.Validate(user);

        public async Task AddToRoleAsync(LibraryUser user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }
        public async Task<LibraryUser?> GetUserInRole(string username, string role)
        {
            var user = await _userManager.FindByNameAsync(username);
            return (user != null && await _userManager.IsInRoleAsync(user, role)) ?
                user : null;
        }
    }
}
