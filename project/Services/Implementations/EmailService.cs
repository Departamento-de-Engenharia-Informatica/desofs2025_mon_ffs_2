using AMAPP.API.Configurations;
using AMAPP.API.DTOs;
using AMAPP.API.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace AMAPP.API.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailService(IOptions<EmailConfiguration> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }

        public async Task SendEmailAsync(MessageDto message)
        {
            MimeMessage emailMessage = CreateEmailMessage(message);
            await SendEmail(emailMessage);
        }

        private MimeMessage CreateEmailMessage(MessageDto message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From, Environment.GetEnvironmentVariable(_emailConfig.EmailEnvUsername, EnvironmentVariableTarget.Machine) ));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(TextFormat.Html) { Text = message.Body };

            return emailMessage;
        }

        private async Task SendEmail(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(Environment.GetEnvironmentVariable(_emailConfig.EmailEnvUsername, EnvironmentVariableTarget.Machine), Environment.GetEnvironmentVariable(_emailConfig.EmailEnvPassword, EnvironmentVariableTarget.Machine));
                    await client.SendAsync(emailMessage);
                    Console.WriteLine("Enviou email");

                }
                catch (Exception)
                {
                    //log an error message or throw and exception or both
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }

            }


        }
    }
}
