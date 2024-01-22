using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.Repositories.Renewals.RenewalCreator
{
    public interface IRenewalCreator
    {
        public Renewal CreateRenewal(Rental rental);
    }
}
