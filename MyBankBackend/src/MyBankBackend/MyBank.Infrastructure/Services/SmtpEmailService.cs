using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MyBank.Application.Interfaces;

namespace MyBank.Infrastructure.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public SmtpEmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task SendPasswordResetEmailAsync(string email, string token)
        {
            using var client = new SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort)
            {
                Credentials = new NetworkCredential(
                    _emailSettings.SmtpUser,
                    _emailSettings.SmtpPass),
                EnableSsl = true
            };

            var message = new MailMessage
            {
                From = new MailAddress(_emailSettings.FromEmail),
                Subject = "Redefinição de Senha - MyBank",
                Body = $"Use o seguinte token para redefinir sua senha: {token}\n" +
                       $"O token expira em 1 hora.",
                IsBodyHtml = false
            };
            message.To.Add(email);

            await client.SendMailAsync(message);
        }
    }
}