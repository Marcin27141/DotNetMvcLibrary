namespace LibraryApp.Models.Database.Entities
{
    public class Penalty
    {
        public int PenaltyId { get; set; }
        public decimal Amount { get; set; }
        public Reader Reader { get; set; }
        public string ReaderId { get; set; }
        public DateOnly ImpositionDate { get; set; }

        public Payment? Payment { get; set; }
        public int? PaymentId { get; set; }

    }
}
