namespace CarRentalSystem.Model
{
    public class Customer
    {
        private int customerID;
        private string firstName;
        private string lastName;
        private string email;
        private string phoneNumber;

        // Default Constructor
        public Customer() { }

        // Parameterized Constructor
        public Customer(int customerID, string firstName, string lastName, string email, string phoneNumber)
        {
            this.customerID = customerID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phoneNumber = phoneNumber;
        }

        // Properties
        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public override string ToString()
        {
            return $"Customer ID: {CustomerID}, Name: {FirstName} {LastName}, " +
                   $"Email: {Email}, Phone: {PhoneNumber}";
        }
    }
}



