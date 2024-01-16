namespace LibraryApp.Models.Database.Entities
{
    public class Renewal
    {
        public int RenewalId { get; set; }
        public DateOnly RenewalDate { get; set; }
        public DateOnly NewReturnDeadline { get; set; }

        //relations
        public int RentalId { get; set; }
        public Rental Rental { get; set; }
    }
}
