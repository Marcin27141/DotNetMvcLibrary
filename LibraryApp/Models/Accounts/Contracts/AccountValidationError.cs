namespace LibraryApp.Models.Accounts.Contracts
{
    public class AccountValidationError(string propertyName, string description)
    {
        public string PropertyName { get; set; } = propertyName;
        public string Description { get; set; } = description;
    }
}
