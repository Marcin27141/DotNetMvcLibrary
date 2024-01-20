using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.ViewModels
{
    public class ConfirmRenewalViewModel
    {
        public Rental Rental { get; set; }
        public int RenewalSpanInDays { get; set; }
        public int RemainingRenewals { get; set; }
    }
}
