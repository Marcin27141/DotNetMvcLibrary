using LibraryApp.Models.Repositories.Renewals.RenewalErrors;

namespace LibraryApp.Models.Repositories.Renewals
{
    public class RenewalsLimitReachedError : RenewalError
    {
        public int Limit { get; set; }
    }
}