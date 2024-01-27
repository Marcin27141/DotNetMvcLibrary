using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.EmailSender
{
    public interface ILibraryEmailSender
    {
        Task SendConfirmationLinkAsync(LibraryUser user, string link);
    }
}
