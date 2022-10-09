namespace UserManagement.Repositories
{
    public class EmailRepository
    {
        private List<string> reservedEmails = new List<string>();
        private List<string> registeredEmails = new List<string>();

        public bool EmailReserved(string email)
        {
            return reservedEmails.Contains(email);
        }

        public void AddReservedEmail(string email)
        {
            reservedEmails.Add(email);
        }

        public void RemoveReservedEmail(string email)
        {
            reservedEmails.Remove(email);
        }

        public bool RegisteredEmail(string email)
        {
            return registeredEmails.Contains(email);
        }

        public void AddRegisteredEmail(string email)
        {
            registeredEmails.Add(email);
        }
    }
}