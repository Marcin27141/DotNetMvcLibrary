using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using LibraryApp.Models.Database.Entities;
using System.Data;
using LibraryApp.Models.ViewModels;
using LibraryApp.Models.Accounts;
using LibraryApp.Models.Repositories.Readers;
using LibraryApp.Models.Accounts.AccountValidator;

namespace LibraryApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountValidator _accountValidator;

        private RegisterViewModel _input;


        public AccountController(
            IAccountRepository accountRepository,
            IAccountValidator accountValidator)
        {
            _accountRepository = accountRepository;
            _accountValidator = accountValidator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LinkSent()
        {
            return View(new LinkSentViewModel(_accountRepository.ActivationLinkValidityInHours));
        }

        public async Task<IActionResult> RegisterReader(
            [FromForm] RegisterViewModel input,
            [FromServices] IReaderRepository readerRepository)
        {
            _input = input;
            if (ModelState.IsValid)
            {
                var userValidation = _accountValidator.Validate(input);
                if (userValidation.IsSuccess)
                {
                    var user = CreateUser();
                    Reader reader = CreateReader(user);
                    var wasCreated = await TryCreateReader(reader, readerRepository);
                    if (wasCreated)
                        return RedirectToAction(nameof(LinkSent));
                }
                else AddValidationErrorsToModelState(userValidation.Errors);
            }

            return View(nameof(Index));
        }

        private async Task<bool> TryCreateReader(Reader reader, IReaderRepository readerRepository)
        {
            var creationResult = await readerRepository.CreateReaderAsync(reader, _input.Password);

            if (creationResult.Succeeded)
            {
                var confirmLink = await GetConfirmLinkAsync(reader.LibraryUser);

                await _accountRepository.SendConfirmationLinkAsync(reader.LibraryUser, confirmLink);
                return true;
            }
            else
            {
                AddIdentityErrorsToModelState(creationResult.Errors);
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
            return HtmlEncoder.Default.Encode(link ?? "Contact our support team");
        }

        private void AddIdentityErrorsToModelState(IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private void AddValidationErrorsToModelState(IEnumerable<AccountValidationError> errors)
        {
            foreach (var errorGroup in errors.GroupBy(e => e.PropertyName))
            {
                ModelState.AddModelError(errorGroup.Key, string.Join(", ", errorGroup.Select(e => e.Description)));
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
            user.Email = _input.Email;
            user.UserName = _input.Email;
            user.FirstName = _input.FirstName;
            user.LastName = _input.LastName;
            user.Birthday = _input.Birthday;
            user.CreationDate = DateOnly.FromDateTime(DateTime.Today);
            user.Status = "Inactive";
            user.Role = "Reader";
            //for testing
            user.EmailConfirmed = true;

            return user;
        }
    }
}
