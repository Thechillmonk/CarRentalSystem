namespace CarRentalSystem.Exceptions
{
    public class LeaseNotFoundException : System.Exception
    {
        // Default constructor
        public LeaseNotFoundException() : base("Lease not found.")
        {
        }

        // Constructor with custom message
        public LeaseNotFoundException(string message) : base(message)
        {
        }

        // Constructor with custom message and inner exception
        public LeaseNotFoundException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
