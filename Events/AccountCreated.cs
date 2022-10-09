using NServiceBus;

namespace UserManagement.Events
{
    public class AccountCreated : IEvent
    {
        public string Email { get; set; }
    }
}