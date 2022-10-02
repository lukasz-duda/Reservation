namespace Reservation
{
    public class UserRepository
    {
        private List<Account> accounts;

        public UserRepository()
        {
            accounts = new List<Account>();
        }

        public void Add(Account account)
        {
            accounts.Add(account);
        }

        public void Remove(Account account)
        {
            accounts.Remove(account);
        }
    }
}