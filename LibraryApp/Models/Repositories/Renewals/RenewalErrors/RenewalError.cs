namespace LibraryApp.Models.Repositories.Renewals.RenewalErrors
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