using LibraryApp.Models.Accounts.Contracts;
using LibraryApp.Models.ViewModels;

namespace LibraryApp.Models.Accounts.AccountVerifiers
{
    public class UserAgeVerifier : IAccountVerifier
    {
        private const int MINIMUM_AGE = 16;
        public AccountValidationResult VerifyAccount(RegisterViewModel user)
        {
            return GetAge(user) < MINIMUM_AGE ?
                AccountValidationResult.Failure(new AccountValidationError(
                    nameof(user.Birthday),
                    "Minimum age is " + MINIMUM_AGE)) :
                AccountValidationResult.Success();
        }

        private static int GetAge(RegisterViewModel user)
        {
            var age = DateTime.Now.Year - user.Birthday.Year;
            if (DateTime.Now.DayOfYear < user.Birthday.DayOfYear)
                age--;

            return age;
        }
    }
}
