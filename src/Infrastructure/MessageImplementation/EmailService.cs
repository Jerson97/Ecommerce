using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Models.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Ecommerce.Infrastructure.MessageImplementation
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettings { get; }

        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }
        public async Task<bool> SendEmail(EmailMessage email, string token)
        {
            try
            {
                using (var client = new SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort))
                {
                    client.Credentials = new NetworkCredential(_emailSettings.SmtpUser, _emailSettings.SmtpPass);
                    client.EnableSsl = true;  // Si tu servidor requiere SSL

                    var from = new MailAddress(_emailSettings.Email!, _emailSettings.DisplayName);
                    var to = new MailAddress(email.To!);
                    var mailMessage = new MailMessage
                    {
                        From = from,
                        Subject = email.Subject,
                        Body = $"{email.Body} {_emailSettings.BaseUrlClient}/password/reset/{token}",
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(to);

                    await client.SendMailAsync(mailMessage);
                    return true;
                }
            }
            catch (SmtpException smtpEx)
            {
                _logger.LogError(smtpEx, "Error SMTP: {Message}", smtpEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error general: {Message}", ex.Message);
                return false;
            }

        }
    }
}
