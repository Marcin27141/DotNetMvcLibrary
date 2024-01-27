using LibraryApp.Models.Renewals.Contracts;

namespace LibraryApp.Models.Renewals.RenewalErrors
{
    public class HasUnpaidPenaltiesError : RenewalError
    {
        public int NumberOfUnpaidPenalties { get; set; }
    }
}