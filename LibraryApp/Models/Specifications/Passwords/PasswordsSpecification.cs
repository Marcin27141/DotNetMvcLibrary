namespace LibraryApp.Models.Specifications.Passwords
{
    public class PasswordsSpecification : IPasswordsSpecification
    {
        public int MinimumLength => 2;
        public bool DigitsRequired => true;
        public bool NonAlphanumericRequired => true;

    }
}
