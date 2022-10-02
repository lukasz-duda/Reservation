namespace Reservation;

public class UserRegistration
{
    private readonly UsernameReservation reservation;
    private readonly UserRepository repository;

    public UserRegistration(UsernameReservation reservation, UserRepository repository)
    {
        this.reservation = reservation;
        this.repository = repository;
    }

    public bool Register(string username)
    {
        bool reserved = reservation.Reserve(username);

        var account = new Account(username);
        repository.Add(account);

        if(reservation.Complete(username) == false)
        {
            repository.Remove(account);
            return false;
        }

        return true;
    }
}