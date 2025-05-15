using MyBank.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // Implementação do envio por email
            await _emailService.SendEmailAsync(
                to: "user@example.com", // Substituir por lógica para obter email do usuário
                subject: title,
                body: message);
        }
    }
}