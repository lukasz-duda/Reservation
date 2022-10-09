using NServiceBus;

namespace UserManagement.Commands
{
    public class SendConfirmationEmail : ICommand
    {
        public string Email { get; set; }
    }
}