using NServiceBus;

public class ReserveEmail : ICommand
{
    public string Email { get; set; }
}