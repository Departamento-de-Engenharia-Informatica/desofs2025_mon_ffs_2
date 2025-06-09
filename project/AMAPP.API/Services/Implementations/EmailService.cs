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
            var emailMessage = CreateEmailMessage(message);
            await SendEmail(emailMessage);
        }

        private MimeMessage CreateEmailMessage(MessageDto message)
        {
            var emailMessage = new MimeMessage();

            // Usar diretamente o email da configuração ou de variável de ambiente
            var senderEmail = GetEmailCredential(_emailConfig.EmailEnvUsername);
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From, senderEmail));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(TextFormat.Html) { Text = message.Body };

            return emailMessage;
        }

        private async Task SendEmail(MimeMessage emailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, SecureSocketOptions.StartTls);

                var username = GetEmailCredential(_emailConfig.EmailEnvUsername);
                var password = GetEmailCredential(_emailConfig.EmailEnvPassword);

                await client.AuthenticateAsync(username, password);
                await client.SendAsync(emailMessage);

                Console.WriteLine("Email enviado com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar email: {ex.Message}");
                throw new InvalidOperationException("Falha ao enviar email", ex);
            }
            finally
            {
                if (client.IsConnected)
                    await client.DisconnectAsync(true);
                // Não precisa chamar Dispose() manualmente devido ao 'using'
            }
        }

        private string GetEmailCredential(string configValue)
        {
            // Primeiro tenta buscar como variável de ambiente
            var envValue = Environment.GetEnvironmentVariable(configValue, EnvironmentVariableTarget.Machine);

            // Se não encontrar, usa o valor da configuração diretamente
            return !string.IsNullOrEmpty(envValue) ? envValue : configValue;
        }
    }
}