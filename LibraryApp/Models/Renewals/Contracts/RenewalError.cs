namespace LibraryApp.Models.Renewals.Contracts
{
    public class RenewalError
    {
        public string Description { get; set; }
        public static RenewalError InvalidRentalError()
        {
            return new RenewalError
            {
                Description = "Rental for this renewal was invalid"
            };
        }
    }
}