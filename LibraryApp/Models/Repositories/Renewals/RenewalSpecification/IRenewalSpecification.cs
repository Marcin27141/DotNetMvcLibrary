namespace LibraryApp.Models.Repositories.Renewals.RenewalSpecification
{
    public interface IRenewalSpecification
    {
        public int MaxRenewalsPerRental { get; }
        public int AllowedPenalties { get; }
        public int RenewalSpanInDays { get; }
    }
}
