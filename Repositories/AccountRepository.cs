using UserManagement.Entities;

namespace UserManagement.Repositories
{
    public class AccountRepository
    {
        private List<Account> accounts = new List<Account>();

        public void Add(Account account)
        {
            accounts.Add(account);
        }

        public Account GetByEmail(string email)
        {
            return accounts.SingleOrDefault(account => account.Email == email);
        }

        public void Remove(Account account)
        {
            accounts.Remove(account);
        }
    }
}