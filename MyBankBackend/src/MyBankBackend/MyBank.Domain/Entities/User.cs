
using MyBank.Domain.Enums;

namespace MyBank.Domain.Entities
{
    public class User : EntityBase
    {
        // Propriedades
        public string FullName { get; private set; }
        public string Email { get; private set; }  // Alterado para string temporariamente
        public string Cpf { get; private set; }   // Alterado para string temporariamente
        public string PhoneNumber { get; private set; }
        public List<Notification> Notifications { get; private set; } = new();

        // Construtor
        protected User() { }

        public User(string fullName, string email, string cpf, string phoneNumber)
        {
            FullName = fullName;
            Email = email;
            Cpf = cpf;
            PhoneNumber = phoneNumber;
        }

        // Métodos de Domínio
        public void UpdateContactInfo(string phoneNumber, string email)
        {
            PhoneNumber = phoneNumber;
            Email = email;
            UpdateTimestamps();
        }

        public void AddNotification(string title, string message, NotificationType type)
        {
            Notifications.Add(new Notification(title, message, type, Id));
        }
    }
}