namespace Reservation;

public class UsernameReservation
{
    private readonly UsernameRepository repository;

    public UsernameReservation(UsernameRepository repository)
    {
        this.repository = repository;
    }

    public bool Reserve(string username)
    {
        if(repository.RegisteredUsername(username))
        {
            return false;
        }

        if(repository.ReservedUsername(username))
        {
            return false;
        }

        repository.AddReservedUsername(username);

        Task.Run(async () =>
        {
            TimeSpan timeout = TimeSpan.FromSeconds(3);
            await Task.Delay(timeout);
            Expire(username);
        });

        return true;
    }

    public void Expire(string username)
    {
        repository.RemoveReservedUsername(username);       
    }

    public bool Complete(string username)
    {
        if(repository.ReservedUsername(username) == false)
        {
            return false;
        }

        if(repository.RegisteredUsername(username))
        {
            return false;
        }

        repository.RemoveReservedUsername(username);
        repository.AddRegisteredUsername(username);

        return true;
    }
}