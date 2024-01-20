using LibraryApp.Models.Database.Entities;

namespace LibraryApp.Models.Repositories.EmailSender
{
    public interface ILibraryEmailSender
    {
        Task SendConfirmationLinkAsync(LibraryUser user, string link);
    }
}
