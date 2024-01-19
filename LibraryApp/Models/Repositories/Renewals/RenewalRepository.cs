using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Renewals.RenewalErrors;
using LibraryApp.Models.Repositories.Renewals.RenewalValidators;

namespace LibraryApp.Models.Repositories.Renewals
{
    public class RenewalRepository : IRenewalRepository
    {
        private readonly IEnumerable<IRenewalValidator> _renewalValidators;

        public RenewalRepository(IEnumerable<IRenewalValidator> renewalValidators)
        {
            _renewalValidators = renewalValidators;
        }

        public RenewalValidityCheck IsValidForRenewal(Rental rental)
        {
            var errors = new List<RenewalError>();
            foreach (var validator in _renewalValidators)
            {
                var result = validator.IsValidForRenewal(rental);
                if (result.Error is not null)
                    errors.Add(result.Error);
            }
            return errors.Any() ?
                RenewalValidityCheck.Fail(errors) :
                RenewalValidityCheck.Success();
        }
    }
}
