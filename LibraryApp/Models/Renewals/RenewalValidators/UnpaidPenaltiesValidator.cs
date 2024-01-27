using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Renewals.Contracts;
using LibraryApp.Models.Renewals.RenewalErrors;
using LibraryApp.Models.Repositories.Renewals;
using LibraryApp.Models.Specifications.RenewalSpecification;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Models.Renewals.RenewalValidators
{
    public class UnpaidPenaltiesValidator : IRenewalValidator
    {
        private readonly IRenewalSpecification _renewalSpecification;

        public UnpaidPenaltiesValidator(IRenewalSpecification renewalSpecification)
        {
            _renewalSpecification = renewalSpecification;
        }
        public RenewalValidationResult IsValidForRenewal(Rental rental)
        {
            int unpaidPenalties = rental.Reader.Penalties.Where(p => p.Payment is null).Count();
            return unpaidPenalties <= _renewalSpecification.AllowedPenalties ?
                RenewalValidationResult.Success() :
                RenewalValidationResult.Fail(GenerateRenewalError(unpaidPenalties));
        }

        private RenewalError GenerateRenewalError(int unpaidPenalties)
        {
            return new HasUnpaidPenaltiesError()
            {
                Description = $"You can't renew any books unless you have less then {_renewalSpecification.AllowedPenalties} unpaid penalties",
                NumberOfUnpaidPenalties = unpaidPenalties
            };
        }
    }
}
