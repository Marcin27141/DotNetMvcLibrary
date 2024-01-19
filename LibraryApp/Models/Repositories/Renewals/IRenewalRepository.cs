using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.Repositories.Renewals
{
    public interface IRenewalRepository
    {
        RenewalValidityCheck IsValidForRenewal(Rental rental);
    }
}
