namespace LibraryApp.Models.Database.Entities
{
    public class Payment
    {
        public int PenaltyId { get; set; }
        public Penalty Penalty { get; set; }
    }
}