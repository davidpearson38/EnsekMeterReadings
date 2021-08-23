namespace EnsekMeterReadings.Domain
{
    public class Account
    {
        public Account(int id, string firstName, string lastName)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public int Id { get; }

        public string FirstName { get; }

        public string LastName { get; }
    }
}
