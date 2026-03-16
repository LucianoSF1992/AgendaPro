using Microsoft.AspNetCore.Identity.UI.Services;

namespace AgendaPro.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Implementação temporária para desenvolvimento
            return Task.CompletedTask;
        }
    }
}