namespace CarRentalSystem.Model
{
    public class Lease
    {
        private int leaseID;
        private int vehicleID;
        private int customerID;
        private DateTime startDate;
        private DateTime endDate;
        private string type;
        private decimal totalAmount; // NEW PROPERTY

        // Default Constructor
        public Lease() { }

        // Parameterized Constructor - UPDATED
        public Lease(int leaseID, int vehicleID, int customerID, DateTime startDate, DateTime endDate, string type, decimal totalAmount = 0)
        {
            this.leaseID = leaseID;
            this.vehicleID = vehicleID;
            this.customerID = customerID;
            this.startDate = startDate;
            this.endDate = endDate;
            this.type = type;
            this.totalAmount = totalAmount;
        }

        // Properties
        public int LeaseID
        {
            get { return leaseID; }
            set { leaseID = value; }
        }

        public int VehicleID
        {
            get { return vehicleID; }
            set { vehicleID = value; }
        }

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        // NEW PROPERTY
        public decimal TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }

        public override string ToString()
        {
            return $"Lease ID: {LeaseID}, Vehicle ID: {VehicleID}, Customer ID: {CustomerID}, " +
                   $"Start Date: {StartDate:yyyy-MM-dd}, End Date: {EndDate:yyyy-MM-dd}, Type: {Type}, " +
                   $"Total Amount: ${TotalAmount:F2}"; // UPDATED ToString
        }
    }
}