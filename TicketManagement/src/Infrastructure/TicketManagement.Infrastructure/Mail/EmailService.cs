using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using TicketManagement.Application.Contracts.Infrastructure;
using TicketManagement.Application.Models.Mail;

namespace TicketManagement.Infrastructure.Mail
{
    public class EmailService(IOptions<EmailSettings> mailSettings) : IEmailService
    {
        public EmailSettings Email_Settings { get; } = mailSettings.Value;

        public async Task<bool> SendEmail(Email email)
        {
            SendGridClient sendGridClient = new(Email_Settings.ApiKey);

            string? emailSubject = email.Subject;
            EmailAddress? toEmailAddress = new(email.To);
            string emailBody = email.Body;

            EmailAddress fromEmailAddress = new()
            {
                Email = Email_Settings.FromAddress,
                Name = Email_Settings.FromName
            };

            SendGridMessage? sendGridMessage = MailHelper.CreateSingleEmail(fromEmailAddress, toEmailAddress, emailSubject, emailBody, emailBody);

            Response? response = await sendGridClient.SendEmailAsync(sendGridMessage);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
    }
}
