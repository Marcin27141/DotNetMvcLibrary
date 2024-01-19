using Humanizer;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Accounts;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Database.Generators.Users
{
    public class UsersGenerator : IUserGenerator
    {
        private const string DEFAULT_PASSWORD = "P@ssw0rd";
        private readonly LibraryDbContext _context;
        private readonly IAccountRepository _accountRepository;

        public UsersGenerator(LibraryDbContext context, IAccountRepository accountRepository)
        {
            _context = context;
            _accountRepository = accountRepository;
        }

        public List<LibraryUser> GenerateUsers()
        {
            foreach (var user in GenerateUsersList())
            {
                SeedOneUser(user, DEFAULT_PASSWORD);
            }
            return _context.Users.ToList();
        }

        public void SeedOneUser(LibraryUser user, string password)
        {
            if (_accountRepository.GetReaderByUsername(user.UserName).Result == null)
            {
                var reader = new Reader { LibraryUser = user };
                _accountRepository.CreateReaderAsync(reader, password).Wait();
            }
        }

        private static IEnumerable<LibraryUser> GenerateUsersList()
        {
            foreach (var letter in "abcdef")
            {
                var user = Activator.CreateInstance<LibraryUser>();
                user.Email = $"{letter}@wp.pl";
                user.UserName = user.Email;
                user.FirstName = char.ToUpper(letter) + new string(letter, 4);
                user.LastName = user.FirstName;
                user.Birthday = DateOnly.Parse("01.01.2000");
                user.CreationDate = DateOnly.FromDateTime(DateTime.Today);
                user.Status = "Active";
                user.Role = "Reader";
                user.EmailConfirmed = true;
                
                yield return user;
            }
        }
    }
}
