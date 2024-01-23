using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Renewals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Tests.FakeModels.FakeRepositories
{
    public class FakeRenewalsRepo : IRenewalRepository
    {
        public int GetRemainingRenewals(Rental rental)
        {
            return 2;
        }

        public int GetRenewalSpan()
        {
            return 30;
        }

        public RenewalValidityCheck IsValidForRenewal(Rental rental)
        {
            return RenewalValidityCheck.Success();
        }

        public Task RenewRental(int rentalId)
        {
            return Task.CompletedTask;
        }
    }
}
