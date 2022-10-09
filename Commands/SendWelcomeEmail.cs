using NServiceBus;

namespace UserManagement.Commands
{
    public class SendWelcomeEmail : ICommand
    {
        public string Email { get; set; }
        public DateTime RegistrationStart { get; set; }
    }
}