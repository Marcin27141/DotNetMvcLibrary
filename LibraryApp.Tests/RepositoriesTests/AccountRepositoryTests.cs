using FakeItEasy;
using LibraryApp.Models.Database;
using LibraryApp.Models.Repositories.Renewals.RenewalCreator;
using LibraryApp.Models.Repositories.Renewals.RenewalValidators;
using LibraryApp.Models.Repositories.Renewals;
using LibraryApp.Models.Specifications.RenewalSpecification;
using LibraryApp.Models.Database.Entities;
using FluentAssertions;
using LibraryApp.Models.Accounts;
using Microsoft.AspNetCore.Identity;
using LibraryApp.Models.Accounts.AccountValidator;
using LibraryApp.Models.Repositories.EmailSender;
using System.IO;
using Moq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Tests.RepositoriesTests
{
    public class AccountRepositoryTests
    {
        private LibraryDbContext _context;
        private UserManager<LibraryUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IAccountValidator _accountValidator;
        private ILibraryEmailSender _emailSender;

        private static string _existingRole = "Reader";
        private static LibraryUser _testReadUser = GetTestUser("test1@wp.pl");
        private LibraryUser _testWriteUser = GetTestUser("test2@wp.pl");
        public AccountRepositoryTests()
        {
            _context = FakeDbContext.FakeDbContext.GetFakeDbContext();
            _emailSender = A.Fake<ILibraryEmailSender>();
            _userManager = GetUserManager();
            _roleManager = GetRoleManager();

            if (!_roleManager.RoleExistsAsync(_existingRole).Result)
                _roleManager.CreateAsync(new IdentityRole(_existingRole)).Wait();
            if (_context.Users.Find(_testReadUser.Id) == null)
                _context.Users.Add(_testReadUser);
            _context.SaveChanges();
        }

        private RoleManager<IdentityRole> GetRoleManager()
        {
            return new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context), null, null, null, null);
        }

        private UserManager<LibraryUser> GetUserManager()
        {
            var store = new UserStore<LibraryUser>(_context);
            return new UserManager<LibraryUser>(
                store, null, new PasswordHasher<LibraryUser>(), null, null, null, null, null, null
                );
        }

        private static LibraryUser GetTestUser(string email)
        {
            var user = Activator.CreateInstance<LibraryUser>();
            user.Email = email;
            user.UserName = email;
            user.FirstName = "Test";
            user.LastName = "Test";
            user.Birthday = new DateOnly(2000, 1, 1);
            user.CreationDate = DateOnly.FromDateTime(DateTime.Today);
            user.Status = "Inactive";
            user.Role = "Reader";
            user.EmailConfirmed = true;

            return user;
        }

        private AccountRepository GetAccountRepository() =>
            new AccountRepository(_userManager, _roleManager, _emailSender, _context);

        [Fact]
        public async Task AccountRepository_CreateUserAsync_AddedToDatabase()
        {
            //Arrange
            var password = "password";
            var repo = GetAccountRepository();

            //Act
            await repo.CreateUserAsync(_testWriteUser, password);

            //Assert
            _context.Users.Find(_testWriteUser.Id).Should().NotBeNull();
        }

        [Fact]
        public async Task AccountRepository_AddToRoleAsync_AddedToRole()
        {
            //Arrange
            var repo = GetAccountRepository();

            //Act
            await repo.AddToRoleAsync(_testReadUser, _existingRole);

            //Assert
            var isInRole = await _userManager.IsInRoleAsync(_testReadUser, _existingRole);
            isInRole.Should().Be(true);
        }

        [Fact]
        public async Task AccountRepository_AddToRoleAsync_IgnoreIfNonexistingRole()
        {
            //Arrange
            var repo = GetAccountRepository();

            //Act
            await repo.AddToRoleAsync(_testReadUser, "NonexistingRole");

            //Assert
            var isInRole = await _userManager.IsInRoleAsync(_testReadUser, _existingRole);
            isInRole.Should().Be(false);
        }

        //[Theory]
        //[InlineData(0, 2, 2)]
        //[InlineData(1, 4, 3)]
        //[InlineData(20, 41, 21)]
        //public void RenewalRepository_GetRemainingRenewals_CorrectValue(int currentRenewals, int maxRenewals, int expected)
        //{
        //    //Arrange
        //    A.CallTo(() => _renewalSpecification.MaxRenewalsPerRental).Returns(maxRenewals);
        //    var repo = GetRenewalRepository();
        //    var rental = new Rental() { Renewals = Enumerable.Repeat(new Renewal(), currentRenewals).ToList() };

        //    //Act
        //    int remaining = repo.GetRemainingRenewals(rental);

        //    //Assert
        //    remaining.Should().Be(expected);
        //}

        //[Fact]
        //public void RenewalRepository_IsValidForRenewal_TrueWhenNoValidators()
        //{
        //    //Arrange
        //    var repo = GetRenewalRepository();
        //    var rental = new Rental();

        //    //Act
        //    var validityCheck = repo.IsValidForRenewal(rental);

        //    //Assert
        //    validityCheck.IsValidForRenewal.Should().Be(true);
        //}

        //[Theory]
        //[InlineData(30)]
        //[InlineData(10)]
        //[InlineData(1)]
        //[InlineData(100)]
        //public void RenewalRepository_RenewRental_CorrectlyRenewed(int renewalSpan)
        //{
        //    //Arrange
        //    A.CallTo(() => _renewalSpecification.RenewalSpanInDays).Returns(renewalSpan);
        //    var repo = GetRenewalRepository();
        //    var rental = _context.Rentals.Find(TEST_RENTAL_ID);
        //    var previousDeadline = rental?.CurrentDeadline;
        //    A.CallTo(() => _renewalCreator.CreateRenewal(rental)).Returns(
        //        new Renewal() { NewReturnDeadline = previousDeadline.Value.AddDays(renewalSpan)}
        //        );

        //    //Act
        //    var validityCheck = repo.RenewRental(TEST_RENTAL_ID);

        //    //Assert
        //    var newDeadline = _context.Rentals.Find(TEST_RENTAL_ID)?.CurrentDeadline;
        //    newDeadline.Should().Be(previousDeadline.Value.AddDays(renewalSpan));
        //}
    }
}
