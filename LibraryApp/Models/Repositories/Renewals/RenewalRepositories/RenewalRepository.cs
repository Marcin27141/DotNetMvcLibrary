using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Renewals.Contracts;
using LibraryApp.Models.Repositories.Renewals.Contracts;
using LibraryApp.Models.Specifications.RenewalSpecification;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Models.Repositories.Renewals.RenewalRepositories
{
    public class RenewalRepository : IRenewalRepository
    {
        private readonly LibraryDbContext _context;
        private readonly IRenewalSpecification _renewalSpecification;
        private readonly IRenewalCreator _renewalCreator;
        private readonly IEnumerable<IRenewalValidator> _renewalValidators;

        public RenewalRepository(
            LibraryDbContext context,
            IRenewalSpecification renewalSpecification,
            IRenewalCreator renewalCreator,
            IEnumerable<IRenewalValidator> renewalValidators)
        {
            _context = context;
            _renewalSpecification = renewalSpecification;
            _renewalCreator = renewalCreator;
            _renewalValidators = renewalValidators;
        }

        public int GetRemainingRenewals(Rental rental)
        {
            var allowed = _renewalSpecification.MaxRenewalsPerRental;
            return Math.Max(0, allowed - rental.Renewals.Count);
        }

        public int GetRenewalSpan() => _renewalSpecification.RenewalSpanInDays;

        public RenewalValidityCheck IsValidForRenewal(Rental rental)
        {
            var errors = new List<RenewalError>();
            foreach (var validator in _renewalValidators)
            {
                var result = validator.IsValidForRenewal(rental);
                if (result.Error is not null)
                    errors.Add(result.Error);
            }
            return errors.Count > 0 ?
                RenewalValidityCheck.Fail(errors) :
                RenewalValidityCheck.Success();
        }

        public async Task RenewRental(int rentalId)
        {
            var rental = await _context.Rentals
                .Include(r => r.Renewals)
                .SingleOrDefaultAsync(r => r.RentalId == rentalId);
            if (rental != null)
            {
                var renewal = _renewalCreator.CreateRenewal(rental);
                rental.Renewals.Add(renewal);
                rental.CurrentDeadline = renewal.NewReturnDeadline;
                await _context.SaveChangesAsync();
            }
        }
    }
}
