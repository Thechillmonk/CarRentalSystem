namespace CarRentalSystem.Exceptions
{
    public class CarNotFoundException : System.Exception
    {
        // Default constructor
        public CarNotFoundException() : base("Car not found.")
        {
        }

        // Constructor with custom message
        public CarNotFoundException(string message) : base(message)
        {
        }

        // Constructor with custom message and inner exception
        public CarNotFoundException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
