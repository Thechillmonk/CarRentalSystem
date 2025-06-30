namespace CarRentalSystem.Exceptions
{
    public class CustomerNotFoundException : System.Exception
    {
        // Default constructor
        public CustomerNotFoundException() : base("Customer not found.")
        {
        }

        // Constructor with custom message
        public CustomerNotFoundException(string message) : base(message)
        {
        }

        // Constructor with custom message and inner exception
        public CustomerNotFoundException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
