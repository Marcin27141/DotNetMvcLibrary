using LibraryApp.Models.Repositories.Renewals.RenewalErrors;

namespace LibraryApp.Models.Repositories.Renewals
{
    public class HasUnpaidPenaltiesError : RenewalError
    {
        public int NumberOfUnpaidPenalties { get; set; }
    }
}