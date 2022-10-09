using NServiceBus;

namespace UserManagement.Handlers
{
    public class UserRegistrationSagaData : ContainSagaData
    {
        public string Email { get; set; }
        public DateTime RegistrationStart { get; set; }
    }
}