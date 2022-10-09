using NServiceBus;
using NServiceBus.Logging;
using UserManagement.Commands;

namespace UserManagement.Handlers
{
    public class SendWelcomeEmailHandler : IHandleMessages<SendWelcomeEmail>
    {
        static ILog log = LogManager.GetLogger<SendWelcomeEmailHandler>();

        public Task Handle(SendWelcomeEmail message, IMessageHandlerContext context)
        {
            log.Info($"Completed {message.Email} registration started {message.RegistrationStart}.");
            log.Info($"Welcome message sent to registered user {message.Email}.");
            return Task.CompletedTask;
        }
    }
}