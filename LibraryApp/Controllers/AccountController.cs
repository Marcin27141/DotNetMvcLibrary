using LibraryApp.Models.Accounts.Contracts;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Accounts;
using LibraryApp.Models.Repositories.Readers;
using LibraryApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Data;
using System.Text;
using System.Text.Encodings.Web;

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

        [HttpGet]
        public IActionResult Index() => RedirectToAction(nameof(RegisterReader));

        public IActionResult LinkSent()
        {
            return View(new LinkSentViewModel(_accountRepository.ActivationLinkValidityInHours));
        }

        [HttpGet]
        public IActionResult RegisterReader() => View();

        [HttpPost]
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
                    Reader reader = CreateReader();
                    var creationResult = await readerRepository.CreateReaderAsync(reader, _input.Password);
                    await HandleCreationResult(creationResult, reader);
                    if (creationResult.Succeeded)
                        return RedirectToAction(nameof(LinkSent));
                }
                else AddValidationErrorsToModelState(userValidation.Errors);
            }

            return View();
        }

        private async Task HandleCreationResult(IdentityResult creationResult, Reader reader)
        {
            if (creationResult.Succeeded)
                await SendEmail(reader);
            else AddIdentityErrorsToModelState(creationResult.Errors);
        }

        private async Task SendEmail(Reader reader)
        {
            var confirmLink = await GetConfirmLinkAsync(reader.LibraryUser);
            await _accountRepository.SendConfirmationLinkAsync(reader.LibraryUser, confirmLink);
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

        private Reader CreateReader()
        {
            return new()
            {
                LibraryUser = CreateUser(),
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

            return user;
        }
    }
}
