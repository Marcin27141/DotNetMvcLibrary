using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Accounts.AccountValidator;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Repositories.Accounts.AccountVerifiers
{
    public class UserAgeVerifier : IAccountVerifier
    {
        private const int MINIMUM_AGE = 16;
        public IdentityResult VerifyAccount(LibraryUser user)
        {
            return GetAge(user) < MINIMUM_AGE ?
                IdentityResult.Failed(new IdentityError()
                {
                    Description = "Minimum age is " + MINIMUM_AGE
                }) :
                IdentityResult.Success;
        }

        private static int GetAge(LibraryUser user)
        {
            var age = DateTime.Now.Year - user.Birthday.Year;
            if (DateTime.Now.DayOfYear < user.Birthday.DayOfYear)
                age--;

            return age;
        }
    }
}
