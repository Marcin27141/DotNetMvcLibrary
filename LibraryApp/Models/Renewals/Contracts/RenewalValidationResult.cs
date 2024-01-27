namespace LibraryApp.Models.Renewals.Contracts
{
    public class RenewalValidationResult
    {
        public bool IsSuccess { get; private set; }
        public RenewalError? Error { get; private set; }

        private RenewalValidationResult(bool isSuccess, RenewalError? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static RenewalValidationResult Success()
        {
            return new RenewalValidationResult(true, null);
        }

        public static RenewalValidationResult Fail(RenewalError error)
        {
            return new RenewalValidationResult(false, error);
        }
    }
}
