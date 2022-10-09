using NServiceBus;
using NServiceBus.Logging;
using UserManagement.Commands;
using UserManagement.Entities;
using UserManagement.Repositories;

namespace UserManagement.Handlers
{
    public class RemoveAccountHandler : IHandleMessages<RemoveAccount>
    {
        private readonly AccountRepository repository;

        static ILog log = LogManager.GetLogger<RemoveAccountHandler>();


        public RemoveAccountHandler(AccountRepository repository)
        {
            this.repository = repository;
        }

        public Task Handle(RemoveAccount message, IMessageHandlerContext context)
        {
            Account account = repository.GetByEmail(message.Email);
            if (account != null)
            {
                repository.Remove(account);
                log.Info($"Account {message.Email} removed.");
            }

            return Task.CompletedTask;
        }
    }
}