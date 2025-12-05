using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace GBC_TicketingSystem_2.Services
{
    public class FakeEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Fake sender, does nothing
            return Task.CompletedTask;
        }
    }
}