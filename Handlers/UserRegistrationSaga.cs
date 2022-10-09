using NServiceBus;
using NServiceBus.Logging;
using UserManagement.Commands;
using UserManagement.Events;

namespace UserManagement.Handlers
{
    public class UserRegistrationSaga : Saga<UserRegistrationSagaData>,
        IAmStartedByMessages<RegisterUser>,
        IHandleMessages<EmailReserved>,
        IHandleMessages<AccountCreated>,
        IHandleMessages<EmailReservationExpired>,
        IHandleMessages<EmailRegistered>
    {
        static ILog log = LogManager.GetLogger<UserRegistrationSaga>();

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<UserRegistrationSagaData> mapper)
        {
            mapper.MapSaga(sagaData => sagaData.Email)
                .ToMessage<RegisterUser>(message => message.Email)
                .ToMessage<EmailReserved>(message => message.Email)
                .ToMessage<AccountCreated>(message => message.Email)
                .ToMessage<EmailReservationExpired>(message => message.Email)
                .ToMessage<EmailRegistered>(message => message.Email);
        }

        public async Task Handle(RegisterUser message, IMessageHandlerContext context)
        {
            Data.RegistrationStart = DateTime.Now;
            await context.Send(new ReserveEmail { Email = message.Email }).ConfigureAwait(false);
        }

        public async Task Handle(EmailReserved message, IMessageHandlerContext context)
        {
            await context.Send(new CreateAccount { Email = message.Email }).ConfigureAwait(false);
        }

        public async Task Handle(AccountCreated message, IMessageHandlerContext context)
        {
            await context.Send(new SendConfirmationEmail { Email = message.Email }).ConfigureAwait(false);
        }

        public async Task Handle(EmailReservationExpired message, IMessageHandlerContext context)
        {
            await context.Send(new RemoveAccount { Email = message.Email }).ConfigureAwait(false);
            MarkAsComplete();
        }

        public async Task Handle(EmailRegistered message, IMessageHandlerContext context)
        {
            await context.Send(new SendWelcomeEmail { Email = message.Email, RegistrationStart = Data.RegistrationStart }).ConfigureAwait(false);
            MarkAsComplete();
        }
    }
}