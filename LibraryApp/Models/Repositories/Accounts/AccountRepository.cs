using Azure.Core;
using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
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
        private readonly SignInManager<LibraryUser> _signInManager;
        private readonly UserManager<LibraryUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly LibraryDbContext _context;

        public AccountRepository(
            UserManager<LibraryUser> userManager,
            SignInManager<LibraryUser> signInManager,
            IEmailSender emailSender,
            LibraryDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

        public async Task SendConfirmationLinkAsync(LibraryUser user)
        {
            //if (user.Email != null)
            //{
            //    await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
            //            $"Please confirm your account by clicking here</a>.");
            //}

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
    }
}
