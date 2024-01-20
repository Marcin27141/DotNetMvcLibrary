using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.Repositories.EmailSender
{
    public class LibraryEmailSender : ILibraryEmailSender
    {
        public Task SendConfirmationLinkAsync(LibraryUser user, string link)
        {
            throw new NotImplementedException();
        }
    }
}
