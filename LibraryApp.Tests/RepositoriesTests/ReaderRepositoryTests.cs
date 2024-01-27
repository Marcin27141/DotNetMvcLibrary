using FakeItEasy;
using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using FluentAssertions;
using LibraryApp.Models.Repositories.Readers;
using Microsoft.AspNetCore.Identity;
using LibraryApp.Models.Repositories.Accounts;

namespace LibraryApp.Tests.RepositoriesTests
{
    public class ReaderRepositoryTests
    {
        private LibraryDbContext _context;
        private IAccountRepository _accountRepository;
        private readonly Reader _testReader = new Reader()
        {
            LibraryUser = GetTestUser(),
            IsActive = false
        };

        public ReaderRepositoryTests()
        {
            _context = FakeDbContext.FakeDbContext.GetFakeDbContext();
            _accountRepository = A.Fake<IAccountRepository>();

            if (_context.Readers.Find(_testReader.LibraryUserId) != null)
            {
                _context.Readers.Remove(_testReader);
                _context.SaveChanges();
            }
        }

        private static LibraryUser GetTestUser()
        {
            var user = Activator.CreateInstance<LibraryUser>();
            user.Email = "test@wp.pl";
            user.UserName = "test@wp.pl";
            user.FirstName = "Test";
            user.LastName = "Test";
            user.Birthday = new DateOnly(2000, 1, 1);
            user.CreationDate = DateOnly.FromDateTime(DateTime.Today);
            user.Status = "Inactive";
            user.Role = "Reader";
            user.EmailConfirmed = true;

            return user;
        }

        

        private ReaderRepository GetReaderRepository() =>
            new ReaderRepository(_context, _accountRepository);

        [Fact]
        public async Task ReaderRepository_CreateReaderAsync_AddedToDatabase()
        {
            //Arrange
            var password = "password";
            A.CallTo(() => _accountRepository.CreateUserAsync(_testReader.LibraryUser, password)).Returns(IdentityResult.Success);
            A.CallTo(() => _accountRepository.GetUserInRole(_testReader.LibraryUser.UserName, "Reader"))
                .Returns(Task.FromResult(_testReader.LibraryUser));
            var repo = GetReaderRepository();

            //Act
            await repo.CreateReaderAsync(_testReader, password);
            var addedReader = await repo.GetReaderByUsername(_testReader.LibraryUser.UserName);

            //Assert
            _context.Readers.Find(_testReader.LibraryUserId).Should().NotBeNull();
            addedReader.Should().Be(_testReader);
        }
    }
}
