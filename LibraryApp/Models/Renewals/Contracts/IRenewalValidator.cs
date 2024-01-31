using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.Renewals.Contracts
{
    public interface IRenewalValidator
    {
        RenewalValidationResult IsValidForRenewal(Rental rental);
    }
}
