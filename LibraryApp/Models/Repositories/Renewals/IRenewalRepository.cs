using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.Repositories.Renewals
{
    public interface IRenewalRepository
    {
        int GetRemainingRenewals(Rental rental);
        int GetRenewalSpan();
        RenewalValidityCheck IsValidForRenewal(Rental rental);
        Task RenewRental(int rentalId);
    }
}
