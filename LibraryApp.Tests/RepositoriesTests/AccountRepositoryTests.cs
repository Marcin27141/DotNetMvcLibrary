using FakeItEasy;
using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LibraryApp.Models.EmailSender;
using LibraryApp.Models.Repositories.Accounts;

namespace LibraryApp.Tests.RepositoriesTests
{
    public class AccountRepositoryTests
    {
        private LibraryDbContext _context;
        private UserManager<LibraryUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ILibraryEmailSender _emailSender;

        private static string _existingRole = "Reader";
        private static string _roleToBeAdded = "Reader2";
        private static LibraryUser _testReadUser = GetTestUser("test1@wp.pl");
        private LibraryUser _testWriteUser = GetTestUser("test2@wp.pl");
        public AccountRepositoryTests()
        {
            _context = FakeDbContext.FakeDbContext.GetFakeDbContext();
            _emailSender = A.Fake<ILibraryEmailSender>();
            _userManager = GetUserManager();
            _roleManager = GetRoleManager();
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

        private void AddRole(string role)
        {
            var roleExists = _roleManager.RoleExistsAsync(role).Result;
            if (!roleExists)
            {
                _roleManager.CreateAsync(new IdentityRole(role)).Wait();
                _context.SaveChanges();
            }
        }

        private async Task AddTestUserAsync()
        {
            if (_context.Users.Find(_testReadUser.Id) == null)
            {
                await _context.Users.AddAsync(_testReadUser);
                await _context.SaveChangesAsync();
                await _userManager.AddToRoleAsync(_testReadUser, _existingRole);
                await _context.SaveChangesAsync();
            }
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
            AddRole(_roleToBeAdded);
            var repo = GetAccountRepository();

            //Act
            await repo.AddToRoleAsync(_testReadUser, _roleToBeAdded);

            //Assert
            var isInRole = await _userManager.IsInRoleAsync(_testReadUser, _roleToBeAdded);
            isInRole.Should().Be(true);
        }

        [Fact]
        public async Task AccountRepository_AddToRoleAsync_IgnoreIfNonexistingRole()
        {
            //Arrange
            var repo = GetAccountRepository();
            var nonexistingRole = "NonexistingRole";

            //Act
            await repo.AddToRoleAsync(_testReadUser, nonexistingRole);

            //Assert
            var isInRole = await _userManager.IsInRoleAsync(_testReadUser, nonexistingRole);
            isInRole.Should().Be(false);
        }

        [Fact]
        public async Task AccountRepository_GetUserInRole_GetNonNullUser()
        {
            //Arrange
            AddRole(_existingRole);
            AddTestUserAsync().Wait();
            var repo = GetAccountRepository();

            //Act
            var user = await repo.GetUserInRole(_testReadUser.UserName, _existingRole);

            //Assert
            user.Should().NotBeNull();
        }

        [Fact]
        public async Task AccountRepository_GetUserInRole_GetNullIfRoleIsAbsent()
        {
            //Arrange
            AddRole(_existingRole);
            AddRole(_roleToBeAdded);
            AddTestUserAsync().Wait();
            var repo = GetAccountRepository();

            //Act
            var user = await repo.GetUserInRole(_testReadUser.UserName, _roleToBeAdded);

            //Assert
            user.Should().BeNull();
        }
    }
}
