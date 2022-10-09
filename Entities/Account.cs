namespace UserManagement.Entities
{
    public class Account
    {
        public Account(string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}