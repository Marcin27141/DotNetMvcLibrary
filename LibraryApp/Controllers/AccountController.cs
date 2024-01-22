using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Accounts;
using System.Data;
using LibraryApp.Models.ViewModels;

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
            return View(new LinkSentViewModel(_accountRepository.ActivationLinkValidityInHours));
        }

        public async Task<IActionResult> Register()
        {
       
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                Reader reader = CreateReader(user);

                var userValidation = _accountRepository.ValidateUser(user);
                if (userValidation.Succeeded)
                {
                    var wasCreated = await TryCreateReader(reader);
                    if (wasCreated) RedirectToAction(nameof(LinkSent));
                }
                else AddCreationErrorsToModelState(userValidation.Errors);
            }

            return View(nameof(Index));
        }

        private async Task<bool> TryCreateReader(Reader reader)
        {
            var creationResult = await _accountRepository.CreateReaderAsync(reader, Input.Password);

            if (creationResult.Succeeded)
            {
                var confirmLink = await GetConfirmLinkAsync(reader.LibraryUser);

                await _accountRepository.SendConfirmationLinkAsync(reader.LibraryUser, confirmLink);
                return true;
            }
            else
            {
                AddCreationErrorsToModelState(creationResult.Errors);
                return false;
            }
        }

        private async Task<string> GetConfirmLinkAsync(LibraryUser user)
        {
            var code = await _accountRepository.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var link = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = user.Id, code },
                protocol: Request.Scheme);
            return HtmlEncoder.Default.Encode(link);
        }

        private void AddCreationErrorsToModelState(IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private static Reader CreateReader(LibraryUser user)
        {
            return new()
            {
                LibraryUser = user,
                IsActive = false
            };
        }

        private LibraryUser CreateUser()
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

            return user;
        }
    }
}
