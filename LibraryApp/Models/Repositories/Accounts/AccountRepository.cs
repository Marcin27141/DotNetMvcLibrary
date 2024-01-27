using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.EmailSender;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Repositories.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<LibraryUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILibraryEmailSender _emailSender;
        private readonly LibraryDbContext _context;

        public int ActivationLinkValidityInHours => 48;

        public AccountRepository(
            UserManager<LibraryUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILibraryEmailSender emailSender,
            LibraryDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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

        public async Task AddToRoleAsync(LibraryUser user, string role)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (roleExists)
                await _userManager.AddToRoleAsync(user, role);
        }
        public async Task<LibraryUser?> GetUserInRole(string username, string role)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user != null && await _userManager.IsInRoleAsync(user, role) ?
                user : null;
        }
    }
}
