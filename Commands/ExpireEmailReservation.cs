using NServiceBus;

namespace UserManagement.Commands
{
    public class ExpireEmailReservation : ICommand
    {
        public string Email { get; set; }
    }
}