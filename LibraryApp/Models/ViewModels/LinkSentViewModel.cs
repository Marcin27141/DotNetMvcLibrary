namespace LibraryApp.Models.ViewModels
{
    public class LinkSentViewModel(int validityTime)
    {
        public int EmailValidityInHours { get; set; } = validityTime;
    }
}
