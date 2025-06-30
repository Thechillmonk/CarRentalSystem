namespace CarRentalSystem.Model
{
    public class Payment
    {
        private int paymentID;
        private int leaseID;
        private DateTime paymentDate;
        private decimal amount;

        // Default Constructor
        public Payment() { }

        // Parameterized Constructor
        public Payment(int paymentID, int leaseID, DateTime paymentDate, decimal amount)
        {
            this.paymentID = paymentID;
            this.leaseID = leaseID;
            this.paymentDate = paymentDate;
            this.amount = amount;
        }

        // Properties
        public int PaymentID
        {
            get { return paymentID; }
            set { paymentID = value; }
        }

        public int LeaseID
        {
            get { return leaseID; }
            set { leaseID = value; }
        }

        public DateTime PaymentDate
        {
            get { return paymentDate; }
            set { paymentDate = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public override string ToString()
        {
            return $"Payment ID: {PaymentID}, Lease ID: {LeaseID}, " +
                   $"Payment Date: {PaymentDate:yyyy-MM-dd}, Amount: ${Amount}";
        }
    }
}
