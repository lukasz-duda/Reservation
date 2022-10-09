using NServiceBus;

namespace UserManagement.Commands
{
    public class CreateAccount : ICommand
    {
        public string Email { get; set; }
    }
}