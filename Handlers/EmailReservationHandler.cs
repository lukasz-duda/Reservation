using NServiceBus;
using NServiceBus.Logging;
using UserManagement.Commands;
using UserManagement.Events;
using UserManagement.Repositories;

namespace UserManagement.Handlers
{
    public class EmailReservationHandler:
        IHandleMessages<ReserveEmail>,
        IHandleMessages<ExpireEmailReservation>,
        IHandleMessages<ConfirmEmail>
    {
        static ILog log = LogManager.GetLogger<EmailReservationHandler>();

        private readonly EmailRepository repository;

        public EmailReservationHandler(EmailRepository repository)
        {
            this.repository = repository;
        }

        public async Task Handle(ReserveEmail message, IMessageHandlerContext context)
        {
            string email = message.Email;

            if (NotAvailable(email))
            {
                log.Info($"{email} not available.");
                return;
            }

            repository.AddReservedEmail(email);

            var expireOptions = new SendOptions();
            expireOptions.DelayDeliveryWith(TimeSpan.FromSeconds(10));
            await context.Send(new ExpireEmailReservation { Email = email }, expireOptions).ConfigureAwait(false);

            await context.Publish(new EmailReserved { Email = email }).ConfigureAwait(false);

        }

        private bool NotAvailable(string email)
        {
            return repository.RegisteredEmail(email) || repository.EmailReserved(email);
        }

        public async Task Handle(ExpireEmailReservation message, IMessageHandlerContext context)
        {
            if(!repository.EmailReserved(message.Email))
            {
                return;
            }
            
            log.Info($"Reservation expired for {message.Email}");
            repository.RemoveReservedEmail(message.Email);
            await context.Publish(new EmailReservationExpired { Email = message.Email }).ConfigureAwait(false);
        }

        public async Task Handle(ConfirmEmail message, IMessageHandlerContext context)
        {
            if (!repository.EmailReserved(message.Email))
            {
                return;
            }

            if (repository.RegisteredEmail(message.Email))
            {
                return;
            }

            repository.RemoveReservedEmail(message.Email);
            repository.AddRegisteredEmail(message.Email);

            await context.Publish(new EmailRegistered { Email = message.Email }).ConfigureAwait(false);
        }
    }
}