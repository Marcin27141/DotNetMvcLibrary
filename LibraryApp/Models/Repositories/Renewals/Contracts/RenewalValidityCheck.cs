using LibraryApp.Models.Renewals.Contracts;

namespace LibraryApp.Models.Repositories.Renewals.Contracts
{
    public class RenewalValidityCheck
    {
        public bool IsValidForRenewal { get; set; }
        public List<RenewalError> Errors { get; set; }

        private RenewalValidityCheck(bool isValid, List<RenewalError> errors)
        {
            IsValidForRenewal = isValid;
            Errors = errors;
        }

        public static RenewalValidityCheck Success()
        {
            return new RenewalValidityCheck(true, new List<RenewalError>());
        }

        public static RenewalValidityCheck Fail(List<RenewalError> errors)
        {
            return new RenewalValidityCheck(false, errors);
        }
    }
}
