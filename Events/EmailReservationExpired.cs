using NServiceBus;

namespace UserManagement.Events
{
    public class EmailReservationExpired : IEvent
    {
        public string Email { get; set; }
    }
}