using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.Database.Generators.Users
{
    public interface IUserGenerator
    {
        List<LibraryUser> GenerateUsers(); 
    }
}
