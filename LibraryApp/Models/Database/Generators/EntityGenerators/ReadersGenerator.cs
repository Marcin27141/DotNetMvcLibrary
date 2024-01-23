using Humanizer;
using LibraryApp.Models.Accounts;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Readers;


namespace LibraryApp.Models.Database.Generators.EntityGenerators
{
    public class ReadersGenerator
    {
        private const string DEFAULT_PASSWORD = "P@ssw0rd";
        private readonly LibraryDbContext _context;
        private readonly IReaderRepository _readersRepository;

        public ReadersGenerator(LibraryDbContext context, IReaderRepository readersRepository)
        {
            _context = context;
            _readersRepository = readersRepository;
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
            if (_readersRepository.GetReaderByUsername(user.UserName).Result == null)
            {
                var reader = new Reader { LibraryUser = user };
                _readersRepository.CreateReaderAsync(reader, password).Wait();
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
