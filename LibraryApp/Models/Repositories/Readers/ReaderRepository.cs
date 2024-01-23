using LibraryApp.Models.Accounts;
using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Models.Repositories.Readers
{
    public class ReaderRepository : IReaderRepository
    {
        private readonly LibraryDbContext _context;
        private readonly IAccountRepository _accountRepository;

        public ReaderRepository(
            LibraryDbContext context,
            IAccountRepository accountRepository)
        {
            _context = context;
            _accountRepository = accountRepository;
        }

        public async Task<IdentityResult> CreateReaderAsync(Reader reader, string password)
        {
            var result = await _accountRepository.CreateUserAsync(reader.LibraryUser, password);
            if (result.Succeeded)
            {
                await _context.Readers.AddAsync(reader);
                await _accountRepository.AddToRoleAsync(reader.LibraryUser, "Reader");
                await _context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<Reader?> GetReaderByUsername(string? username)
        {
            if (username == null) return null;

            var user = await _accountRepository.GetUserInRole(username, "Reader");
            if (user != null)
            {
                return _context.Readers.Find(user.Id);
            }
            return null;
        }
    }
}
