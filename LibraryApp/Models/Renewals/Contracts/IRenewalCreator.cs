using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.Renewals.Contracts
{
    public interface IRenewalCreator
    {
        public Renewal CreateRenewal(Rental rental);
    }
}
