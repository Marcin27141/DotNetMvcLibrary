using LibraryApp.Models.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models.Renewals.Contracts
{
    public interface IRenewalValidator
    {
        RenewalValidationResult IsValidForRenewal(Rental rental);
    }
}
