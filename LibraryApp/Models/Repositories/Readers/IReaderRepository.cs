using LibraryApp.Models.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Repositories.Readers
{
    public interface IReaderRepository
    {
        Task<IdentityResult> CreateReaderAsync(Reader reader, string password);
        Task<Reader?> GetReaderByUsername(string? username);
    }
}
