using iTut.Models.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System.Threading.Tasks;

namespace iTut.Services.Implementation
{
    public class MailService : IMailService
    {
        private readonly MailSettings _settings;
        private readonly ILogger<MailService> _logger;
        public MailService(IOptions<MailSettings> settings, ILoggerFactory loggerFactory)
        {
            _settings = settings.Value;
            _logger = loggerFactory.CreateLogger<MailService>();
        }

        public Task SendEmail(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_settings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.Receiver));
            email.Subject = mailRequest.Subject;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = mailRequest.Body,
            };
            using var client = new SmtpClient();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            client.Connect(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);

            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(_settings.Mail, _settings.Password);

            client.Send(email);
            client.Disconnect(true);

            _logger.LogInformation($"Email: {mailRequest.Receiver}, Subject: {mailRequest.Subject}, Message: {mailRequest.Body}");
            return Task.CompletedTask;
        }
    }
}
