using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Renewals.RenewalErrors;
using LibraryApp.Models.Repositories.Renewals.RenewalValidators;
using LibraryApp.Models.Repositories.Renewals;
using Microsoft.AspNetCore.Identity;
using LibraryApp.Models.ViewModels;
using LibraryApp.Models.Accounts;
using LibraryApp.Models.Accounts.AccountVerifiers;

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
