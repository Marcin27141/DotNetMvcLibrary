using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Renewals.RenewalSpecification;

namespace LibraryApp.Models.Repositories.Renewals.RenewalCreator
{
    public class RenewalCreator : IRenewalCreator
    {
        private readonly IRenewalSpecification _renewalSpecification;

        public RenewalCreator(IRenewalSpecification renewalSpecification)
        {
            _renewalSpecification = renewalSpecification;
        }
        public Renewal CreateRenewal(Rental rental)
        {
            var newDeadline = rental.CurrentDeadline.AddDays(_renewalSpecification.RenewalSpanInDays);
            return new Renewal()
            {
                RentalId = rental.RentalId,
                RenewalDate = DateOnly.FromDateTime(DateTime.Today),
                NewReturnDeadline = newDeadline
            };
        }
    }
}
