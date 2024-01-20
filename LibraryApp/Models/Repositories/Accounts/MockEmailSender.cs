using Microsoft.AspNetCore.Identity.UI.Services;

namespace LibraryApp.Models.Repositories.Accounts
{
    public class MockEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}
