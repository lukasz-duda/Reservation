using NServiceBus;
using UserManagement.Commands;
using UserManagement.Entities;
using UserManagement.Events;
using UserManagement.Repositories;

namespace UserManagement.Handlers
{
    public class CreateAccountHandler : IHandleMessages<CreateAccount>
    {
        private readonly AccountRepository repository;

        public CreateAccountHandler(AccountRepository repository)
        {
            this.repository = repository;
        }

        public async Task Handle(CreateAccount message, IMessageHandlerContext context)
        {
            var account = new Account(message.Email);
            repository.Add(account);
            await context.Publish(new AccountCreated { Email = message.Email }).ConfigureAwait(false);
        }
    }
}