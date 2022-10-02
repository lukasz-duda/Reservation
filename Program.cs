using Reservation;

var usernameRepository = new UsernameRepository();
var reservation = new UsernameReservation(usernameRepository);
var userRepository = new UserRepository();
var registration = new UserRegistration(reservation, userRepository);

static bool TryGetUsername(out string username)
{
    Console.Write("Zarejestruj użytkownika o nazwie: ");
    username = Console.ReadLine() ?? string.Empty;
    return string.IsNullOrWhiteSpace(username) == false;
}

string username;

while (TryGetUsername(out username))
{
    bool registered = registration.Register(username);
    Console.WriteLine(registered ? $"Użytkownik {username} zarejestrowany." : $"Nazwa {username} niedostępna.");
}