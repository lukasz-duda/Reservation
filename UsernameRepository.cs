namespace Reservation
{
    public class UsernameRepository
    {
        private List<string> reservedUsernames;
        private List<string> registeredUsernames;

        public UsernameRepository()
        {
            reservedUsernames = new List<string>();
            registeredUsernames = new List<string>();
        }

        public bool ReservedUsername(string username)
        {
            return reservedUsernames.Contains(username);
        }

        public void AddReservedUsername(string username)
        {
            reservedUsernames.Add(username);
        }

        public void RemoveReservedUsername(string username)
        {
            reservedUsernames.Remove(username);
        }

        public bool RegisteredUsername(string username)
        {
            return registeredUsernames.Contains(username);
        }

        public void AddRegisteredUsername(string username)
        {
            registeredUsernames.Add(username);
        }

        public void RemoveRegisteredUsername(string username)
        {
            registeredUsernames.Remove(username);
        }
    }
}