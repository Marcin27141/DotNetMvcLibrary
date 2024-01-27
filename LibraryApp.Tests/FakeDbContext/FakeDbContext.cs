using LibraryApp.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Tests.FakeDbContext
{
    public class FakeDbContext
    {
        public static LibraryDbContext GetFakeDbContext()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContext = new LibraryDbContext(options);
            dbContext.Database.EnsureCreated();
            return dbContext;
        }
    }
}
