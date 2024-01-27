using FakeItEasy;
using FluentAssertions;
using LibraryApp.Controllers;
using LibraryApp.Models.ViewModels;
using LibraryApp.Models.Repositories.Readers;
using LibraryApp.Models.Accounts.Contracts;
using LibraryApp.Models.Repositories.Accounts;

namespace LibraryApp.Tests.ControllersTests
{
    public class AccountControllerTest
    {
        private IAccountRepository _accountRepository;
        private IAccountValidator _accountValidator;
        public AccountControllerTest()
        {
            _accountRepository = A.Fake<IAccountRepository>();
            _accountValidator = A.Fake<IAccountValidator>();
        }

        private AccountController GetAccountController() =>
            new AccountController(_accountRepository, _accountValidator);

        [Fact]
        public void AccountController_RegisterReader_ModelErrorsOnValidationFailure()
        {
            //Arrange
            var model = new RegisterViewModel();
            var readerRepository = A.Fake<IReaderRepository>();
            A.CallTo(() => _accountValidator.Validate(model)).Returns(AccountValidationResult.Failure(
                new AccountValidationError(string.Empty, string.Empty)));
            var controller = GetAccountController();

            //Act
            var actionResult = controller.RegisterReader(model, readerRepository);

            //Assert
            controller.ModelState.IsValid.Should().BeFalse();
        }
    }
}
