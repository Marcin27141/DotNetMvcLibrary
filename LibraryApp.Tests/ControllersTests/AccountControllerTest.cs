using FakeItEasy;
using FluentAssertions;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Database;
using LibraryApp.Models.Repositories.Renewals.RenewalCreator;
using LibraryApp.Models.Repositories.Renewals.RenewalValidators;
using LibraryApp.Models.Repositories.Renewals;
using LibraryApp.Models.Specifications.RenewalSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Controllers;
using LibraryApp.Models.Repositories.Rentals;
using Microsoft.AspNetCore.Mvc;
using LibraryApp.Models.ViewModels;
using LibraryApp.Models.Repositories.Renewals.RenewalErrors;
using LibraryApp.Models.Accounts;
using LibraryApp.Models.Accounts.AccountValidator;
using LibraryApp.Models.Repositories.Readers;

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
