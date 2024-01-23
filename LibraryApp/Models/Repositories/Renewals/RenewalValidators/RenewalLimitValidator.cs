using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Renewals.RenewalErrors;
using LibraryApp.Models.Specifications.RenewalSpecification;

namespace LibraryApp.Models.Repositories.Renewals.RenewalValidators
{
    public class RenewalLimitValidator : IRenewalValidator
    {
        private readonly IRenewalSpecification _renewalSpecification;

        public RenewalLimitValidator(IRenewalSpecification renewalSpecification)
        {
            _renewalSpecification = renewalSpecification;
        }

        public RenewalValidationResult IsValidForRenewal(Rental rental)
        {
            var renewalsCount = rental.Renewals.Count;

            return renewalsCount < _renewalSpecification.MaxRenewalsPerRental ?
                RenewalValidationResult.Success() :
                RenewalValidationResult.Fail(GenerateRenewalError());
        }

        private RenewalError GenerateRenewalError()
        {
            return new RenewalsLimitReachedError()
            {
                Description = $"You can't renew a book more than {_renewalSpecification.MaxRenewalsPerRental} times",
                Limit = _renewalSpecification.MaxRenewalsPerRental
            };
        }
    }
}
