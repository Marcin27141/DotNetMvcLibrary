using Azure.Core;
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

        public AccountRepository(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<IdentityResult> CreateUserAsync(IdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password); ;
        }

        public async Task SendConfirmationLinkAsync(IdentityUser user)
        {
            //if (user.Email != null)
            //{
            //    await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
            //            $"Please confirm your account by clicking here</a>.");
            //}
            
        }
    }
}
