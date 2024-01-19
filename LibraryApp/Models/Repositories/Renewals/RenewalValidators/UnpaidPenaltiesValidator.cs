using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Renewals.RenewalErrors;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Models.Repositories.Renewals.RenewalValidators
{
    public class UnpaidPenaltiesValidator : IRenewalValidator
    {
        private readonly LibraryDbContext _context;
        private const int MAX_UNPAID_PENALTIES = 2;

        public UnpaidPenaltiesValidator(LibraryDbContext context)
        {
            _context = context;
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
            return unpaidPenalties < MAX_UNPAID_PENALTIES ?
                RenewalValidationResult.Success() :
                RenewalValidationResult.Fail(GenerateRenewalError(unpaidPenalties));
        }

        private static RenewalError GenerateRenewalError(int unpaidPenalties)
        {
            return new HasUnpaidPenaltiesError()
            {
                Description = $"You can't renew any books unless you have less then {MAX_UNPAID_PENALTIES} unpaid penalties",
                NumberOfUnpaidPenalties = unpaidPenalties
            };
        }
    }
}
