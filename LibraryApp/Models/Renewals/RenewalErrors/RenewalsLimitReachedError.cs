using LibraryApp.Models.Renewals.Contracts;

namespace LibraryApp.Models.Renewals.RenewalErrors
{
    public class RenewalsLimitReachedError : RenewalError
    {
        public int Limit { get; set; }
    }
}