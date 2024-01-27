using LibraryApp.Models.ViewModels;
using LibraryApp.Models.Accounts.Contracts;

namespace LibraryApp.Models.Accounts.AccountValidator
{
    public class AccountValidator : IAccountValidator
    {
        private readonly IEnumerable<IAccountVerifier> _accountVerifiers;

        public AccountValidator(IEnumerable<IAccountVerifier> accountVerifiers)
        {
            _accountVerifiers = accountVerifiers;
        }
        public AccountValidationResult Validate(RegisterViewModel user)
        {
            var errors = new List<AccountValidationError>();

            foreach (var verifier in _accountVerifiers)
            {
                var result = verifier.VerifyAccount(user);
                if (!result.IsSuccess)
                    errors.AddRange(result.Errors);
            }

            return errors.Count == 0 ?
                AccountValidationResult.Success() :
                AccountValidationResult.Failure(errors);
        }
    }
}
