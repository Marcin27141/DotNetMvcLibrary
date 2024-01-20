using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.Repositories.Renewals.RenewalCreator
{
    public class RenewalCreator : IRenewalCreator
    {
        private const int RENEWAL_SPAN_IN_DAYS = 30;

        public Renewal CreateRenewal(Rental rental)
        {
            var previousDeadline = rental.Renewals.Any() ? rental.Renewals.Max(r => r.NewReturnDeadline) : rental.OriginalReturnDeadline;
            return new Renewal()
            {
                RentalId = rental.RentalId,
                RenewalDate = DateOnly.FromDateTime(DateTime.Today),
                NewReturnDeadline = previousDeadline.AddDays(RENEWAL_SPAN_IN_DAYS)
            };
        }

        public int GetRenewalSpan() => RENEWAL_SPAN_IN_DAYS;
    }
}
