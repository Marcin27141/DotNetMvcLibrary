namespace LibraryApp.Models.Specifications.RenewalSpecification
{
    public class RenewalSpecification : IRenewalSpecification
    {
        public int MaxRenewalsPerRental => 2;

        public int AllowedPenalties => 0;
        public int RenewalSpanInDays => 30;
    }
}
