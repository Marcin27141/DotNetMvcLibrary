using LibraryApp.Models.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models.Repositories.Renewals.RenewalValidators
{
    public interface IRenewalValidator
    {
        RenewalValidationResult IsValidForRenewal(Rental rental);
    }
}
