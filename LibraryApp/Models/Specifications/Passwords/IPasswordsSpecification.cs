namespace LibraryApp.Models.Specifications.Passwords
{
    public interface IPasswordsSpecification
    {
        public int MinimumLength { get; }
        public bool DigitsRequired { get; }
        public bool NonAlphanumericRequired { get; }
    }
}
