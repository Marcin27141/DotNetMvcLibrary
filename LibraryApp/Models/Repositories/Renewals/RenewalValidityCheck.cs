using LibraryApp.Models.Repositories.Renewals.RenewalErrors;

namespace LibraryApp.Models.Repositories.Renewals
{
    public class RenewalValidityCheck
    {
        public bool IsValidForRenewal { get; set; }
        public List<RenewalError> Errors { get; set; }
    }
}
