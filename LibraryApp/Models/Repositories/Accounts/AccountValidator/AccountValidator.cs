using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Renewals.RenewalErrors;
using LibraryApp.Models.Repositories.Renewals.RenewalValidators;
using LibraryApp.Models.Repositories.Renewals;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Models.Repositories.Accounts.AccountValidator
{
    public class AccountValidator : IAccountValidator
    {
        private readonly IEnumerable<IAccountVerifier> _accountVerifiers;

        public AccountValidator(IEnumerable<IAccountVerifier> accountVerifiers)
        {
            _accountVerifiers = accountVerifiers;
        }
        public IdentityResult Validate(LibraryUser user)
        {
            var errors = new List<IdentityError>();

            foreach (var verifier in _accountVerifiers)
            {
                var result = verifier.VerifyAccount(user);
                if (!result.Succeeded)
                    errors.AddRange(result.Errors);
            }

            return errors.Count == 0 ?
                IdentityResult.Success :
                IdentityResult.Failed(errors.ToArray());
        }
    }
}
