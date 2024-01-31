using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Renewals.Contracts;
using LibraryApp.Models.Specifications.RenewalSpecification;

namespace LibraryApp.Models.Renewals.RenewalCreator
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
