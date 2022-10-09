using NServiceBus;
using NServiceBus.Logging;
using UserManagement.Commands;

namespace UserManagement.Handlers
{
    public class SendConfirmationEmailHandler : IHandleMessages<SendConfirmationEmail>
    {
        static ILog log = LogManager.GetLogger<SendConfirmationEmailHandler>();

        public Task Handle(SendConfirmationEmail message, IMessageHandlerContext context)
        {
            log.Info($"Confirmation email sent to {message.Email}.");
            return Task.CompletedTask;
        }
    }
}