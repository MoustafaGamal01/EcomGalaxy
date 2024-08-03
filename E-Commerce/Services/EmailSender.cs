using EcomGalaxy.Models;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace EcomGalaxy.Services
{
	public class EmailSender : IEmailSender
	{
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var smtpPort = _configuration["EmailSettings:Port"];
            var smtpUsername = _configuration["EmailSettings:Username"];
            var smtpPassword = _configuration["EmailSettings:Password"];
            var smtpFrom = _configuration["EmailSettings:From"];

            if (string.IsNullOrWhiteSpace(smtpServer) ||
                string.IsNullOrWhiteSpace(smtpPort) ||
                string.IsNullOrWhiteSpace(smtpUsername) ||
                string.IsNullOrWhiteSpace(smtpPassword) ||
                string.IsNullOrWhiteSpace(smtpFrom))
            {
                throw new ArgumentException("SMTP configuration is invalid.");
            }

            if (!int.TryParse(smtpPort, out var port))
            {
                throw new ArgumentException("SMTP port is invalid.");
            }

            var smtpClient = new SmtpClient(smtpServer)
            {
                Port = port,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpFrom),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
