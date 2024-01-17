using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.Repositories.Renewals
{
    public interface IRenewalRepository
    {
        Task<RenewalValidityCheck> IsValidForRenewalAsync(Rental rental);
    }
}
