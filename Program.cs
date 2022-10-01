using Reservation;

var reservation = new UsernameReservation();
var repository = new UserRepository();
var registration = new UserRegistration(reservation, repository);

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