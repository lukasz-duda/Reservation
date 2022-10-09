using NServiceBus;

namespace UserManagement.Commands
{
    public class RegisterUser : ICommand
    {
        public string Email { get; set; }
    }
}