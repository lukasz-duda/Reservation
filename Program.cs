using NServiceBus;
using NServiceBus.Logging;
using UserManagement.Commands;
using UserManagement.Handlers;
using UserManagement.Repositories;

namespace UserManagement
{
    class Program
    {
        private const string EndpointName = "UserManagement";
        static ILog log = LogManager.GetLogger<Program>();

        private static async Task Main(string[] args)
        {
            var configuration = new EndpointConfiguration(EndpointName);
            configuration.RegisterComponents(
                registration: configureComponents =>
                {
                    configureComponents.ConfigureComponent<AccountRepository>(DependencyLifecycle.SingleInstance);
                    configureComponents.ConfigureComponent<EmailReservationHandler>(DependencyLifecycle.SingleInstance);
                    configureComponents.ConfigureComponent<EmailRepository>(DependencyLifecycle.SingleInstance);
                });

            var transport = configuration.UseTransport<LearningTransport>();
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(RegisterUser).Assembly, EndpointName);
            configuration.UsePersistence<LearningPersistence>();
            
            var endpoint = await Endpoint.Start(configuration)
                .ConfigureAwait(false);

            while (true)
            {
                Console.WriteLine("Press 'r' to register or 'c' to confirm user.");
                var keyInfo = Console.ReadKey();
                Console.WriteLine();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.R:
                        await RegisterUser(endpoint).ConfigureAwait(false);
                        break;

                    case ConsoleKey.C:
                        await ConfirmEmail(endpoint).ConfigureAwait(false);
                        break;

                    default:
                        await endpoint.Stop().ConfigureAwait(false);
                        return;
                }
            }

        }

        private static async Task ConfirmEmail(IEndpointInstance endpoint)
        {
            Console.WriteLine("Confirm email.");
            string confirmationEmail = GetEmail();
            var confirmEmail = new ConfirmEmail { Email = confirmationEmail };
            await endpoint.Send(confirmEmail).ConfigureAwait(false);
        }

        private static string GetEmail()
        {
            Console.Write("Email: ");
            return Console.ReadLine();
        }

        private static async Task RegisterUser(IEndpointInstance endpoint)
        {
            Console.WriteLine("Register user.");
            string registrationEmail = GetEmail();
            var registerUser = new RegisterUser { Email = registrationEmail };
            await endpoint.Send(registerUser).ConfigureAwait(false);
        }
    }
}