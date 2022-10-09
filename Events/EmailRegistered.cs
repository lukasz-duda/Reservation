using NServiceBus;

namespace UserManagement.Events
{
    public class EmailRegistered : IEvent
    {
        public string Email { get; set; }
    }
}