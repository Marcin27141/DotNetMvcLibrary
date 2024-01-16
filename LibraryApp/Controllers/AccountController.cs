using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using LibraryApp.Models;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Accounts;

namespace LibraryApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        [BindProperty]
        public RegisterViewModel Input { get; set; }


        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LinkSent()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            if (ModelState.IsValid)
            {
                var user = Activator.CreateInstance<IdentityUser>();
                user.Email = Input.Email;
                user.UserName = Input.Email;
                Reader reader = new()
                {
                    LibraryUser = new()
                    {
                        IdentityUser = user,
                        FirstName = Input.FirstName,
                        LastName = Input.LastName,
                        Birthday = Input.Birthday,
                        CreationDate = DateOnly.FromDateTime(DateTime.Today),
                        Status = "Inactive",
                        Role = "Reader"
                    },
                    IsActive = false
                };
                
                var result = await _accountRepository.CreateReaderAsync(reader, Input.Password);

                if (result.Succeeded)
                {
                    await _accountRepository.SendConfirmationLinkAsync(reader.LibraryUser);
                    return RedirectToAction(nameof(LinkSent));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(nameof(Index));
        }
    }
}
