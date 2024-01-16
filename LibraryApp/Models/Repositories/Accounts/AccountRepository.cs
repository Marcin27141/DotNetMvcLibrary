using Azure.Core;
using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Policy;
using System.Text;
using System.Text.Encodings.Web;

namespace LibraryApp.Models.Repositories.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly LibraryDbContext _context;

        public AccountRepository(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
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
            var result = await _userManager.CreateAsync(user.IdentityUser, password);
            if (result.Succeeded)
            {
                await _context.LibraryUsers.AddAsync(user);
                await _context.SaveChangesAsync();
            }
                
            return result;
        }

        public async Task<IdentityResult> CreateReaderAsync(Reader reader, string password)
        {
            var result = await CreateUserAsync(reader.LibraryUser, password);
            if (result.Succeeded)
            {
                await _context.Readers.AddAsync(reader);
                await _context.SaveChangesAsync();
            }
            
            return result;
        }

        public async Task SendConfirmationLinkAsync(LibraryUser user)
        {
            //if (user.Email != null)
            //{
            //    await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
            //            $"Please confirm your account by clicking here</a>.");
            //}

        }
    }
}
