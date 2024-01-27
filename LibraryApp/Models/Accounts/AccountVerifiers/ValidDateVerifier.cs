using LibraryApp.Models.Accounts.Contracts;
using LibraryApp.Models.ViewModels;

namespace LibraryApp.Models.Accounts.AccountVerifiers
{
    public class ValidDateVerifier : IAccountVerifier
    {
        private readonly DateOnly MINIMUM_BIRTHDAY = DateOnly.Parse("01.01.1900");
        public AccountValidationResult VerifyAccount(RegisterViewModel user)
        {
            return user.Birthday > MINIMUM_BIRTHDAY && user.Birthday <= Today() ?
                AccountValidationResult.Success() :
                AccountValidationResult.Failure(new AccountValidationError(
                    nameof(user.Birthday),
                    $"Birthday must be between {MINIMUM_BIRTHDAY} and {Today()}"
                    ));
        }

        private static DateOnly Today() => DateOnly.FromDateTime(DateTime.Now);
    }
}
