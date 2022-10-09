using NServiceBus;

namespace UserManagement.Commands
{
    public class ConfirmEmail : ICommand
    {
        public string Email { get; set; }
    }
}