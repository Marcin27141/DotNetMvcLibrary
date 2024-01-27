using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.EmailSender
{
    public class LibraryEmailSender : ILibraryEmailSender
    {
        public Task SendConfirmationLinkAsync(LibraryUser user, string link)
        {
            return Task.CompletedTask;
        }
    }
}
