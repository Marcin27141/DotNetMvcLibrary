using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Renewals.RenewalErrors;

namespace LibraryApp.Models.Repositories.Renewals.RenewalValidators
{
    public class RenewalLimitValidator : IRenewalValidator
    {
        private const int MAX_RENEWALS_PER_RENTAL = 2;

        public RenewalValidationResult IsValidForRenewal(Rental rental)
        {
            var renewalsCount = rental.Renewals.Count;

            return renewalsCount < MAX_RENEWALS_PER_RENTAL ?
                RenewalValidationResult.Success() :
                RenewalValidationResult.Fail(GenerateRenewalError());
        }

        private static RenewalError GenerateRenewalError()
        {
            return new RenewalsLimitReachedError()
            {
                Description = $"You can't renew a book more than {MAX_RENEWALS_PER_RENTAL} times",
                Limit = MAX_RENEWALS_PER_RENTAL
            };
        }
    }
}
