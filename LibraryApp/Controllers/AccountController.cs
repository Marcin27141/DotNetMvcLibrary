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
using System.Data;

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
            //return RedirectToAction("Index", "Rentals", new { area = "" });
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
                var user = Activator.CreateInstance<LibraryUser>();
                user.Email = Input.Email;
                user.UserName = Input.Email;
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                user.Birthday = Input.Birthday;
                user.CreationDate = DateOnly.FromDateTime(DateTime.Today);
                user.Status = "Inactive";
                user.Role = "Reader";
                //for testing
                user.EmailConfirmed = true;

                Reader reader = new()
                {
                    LibraryUser = user,
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
