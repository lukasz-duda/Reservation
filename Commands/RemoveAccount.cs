using NServiceBus;

namespace UserManagement.Commands
{
    public class RemoveAccount : ICommand
    {
        public string Email { get; set; }
    }
}