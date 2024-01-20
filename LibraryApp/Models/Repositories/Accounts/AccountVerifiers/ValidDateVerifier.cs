using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Accounts.AccountValidator;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Repositories.Accounts.AccountVerifiers
{
    public class ValidDateVerifier : IAccountVerifier
    {
        private readonly DateOnly MINIMUM_BIRTHDAY = DateOnly.Parse("01.01.1900");
        public IdentityResult VerifyAccount(LibraryUser user)
        {
            return user.Birthday > MINIMUM_BIRTHDAY && user.Birthday <= Today() ?
                IdentityResult.Success :
                IdentityResult.Failed(new IdentityError()
                {
                    Description = $"Birthday must be between {MINIMUM_BIRTHDAY} and {Today()}"
                });
        }

        private static DateOnly Today() => DateOnly.FromDateTime(DateTime.Now);
    }
}
