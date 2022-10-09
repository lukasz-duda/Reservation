using NServiceBus;

namespace UserManagement.Events
{
    public class EmailReserved : IEvent
    {
        public string Email { get; set; }
    }
}