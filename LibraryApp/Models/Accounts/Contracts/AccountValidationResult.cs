namespace LibraryApp.Models.Accounts.Contracts
{
    public class AccountValidationResult
    {
        public static AccountValidationResult Success() => new(true, Enumerable.Empty<AccountValidationError>());

        public static AccountValidationResult Failure(IEnumerable<AccountValidationError> errors) => new(false, errors);
        public static AccountValidationResult Failure(AccountValidationError error)
            => new(false, new List<AccountValidationError>() { error });

        private AccountValidationResult(bool successful, IEnumerable<AccountValidationError> errors)
        {
            IsSuccess = successful;
            Errors = errors;
        }

        public bool IsSuccess { get; set; }
        public IEnumerable<AccountValidationError> Errors { get; set; }
    }
}
