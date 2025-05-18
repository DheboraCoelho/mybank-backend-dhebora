using MyBank.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyBank.Infrastructure.Services
{
    public class EmailNotificationSender : INotificationSender
    {
        private readonly IEmailService _emailService;

        public EmailNotificationSender(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendNotificationAsync(string userId, string title, string message)
        {
            // Implementação genérica para enviar e-mails
            await _emailService.SendEmailAsync(
                to: userId, // Aqui você pode precisar obter o e-mail do usuário pelo ID
                subject: title,
                body: message);
        }
    }
}