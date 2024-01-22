using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Renewals.RenewalErrors;
using LibraryApp.Models.Repositories.Renewals.RenewalSpecification;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Models.Repositories.Renewals.RenewalValidators
{
    public class UnpaidPenaltiesValidator : IRenewalValidator
    {
        private readonly LibraryDbContext _context;
        private readonly IRenewalSpecification _renewalSpecification;

        public UnpaidPenaltiesValidator(LibraryDbContext context, IRenewalSpecification renewalSpecification)
        {
            _context = context;
            _renewalSpecification = renewalSpecification;
        }
        public RenewalValidationResult IsValidForRenewal(Rental rental)
        {
            var reader = _context.Readers
                .Include(r => r.Penalties)
                .ThenInclude(p => p.Payment)
                .SingleOrDefault(r => r.LibraryUserId.Equals(rental.ReaderId));

            if (reader == null)
                return RenewalValidationResult.Fail(RenewalError.InvalidRentalError());

            int unpaidPenalties = reader.Penalties.Where(p => p.Payment is null).Count();
            return unpaidPenalties < _renewalSpecification.AllowedPenalties ?
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
