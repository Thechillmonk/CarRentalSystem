namespace CarRentalSystem.Model
{
    public class Vehicle
    {
        private int vehicleID;
        private string make;
        private string model;
        private int year;
        private decimal dailyRate;
        private string status;
        private int passengerCapacity;
        private decimal engineCapacity;

        // Default Constructor - FIXED
        public Vehicle() { }

        // Parameterized Constructor
        public Vehicle(int vehicleID, string make, string model, int year, decimal dailyRate,
                      string status, int passengerCapacity, decimal engineCapacity)
        {
            this.vehicleID = vehicleID;
            this.make = make;
            this.model = model;
            this.year = year;
            this.dailyRate = dailyRate;
            this.status = status;
            this.passengerCapacity = passengerCapacity;
            this.engineCapacity = engineCapacity;
        }

        // Properties (Getters and Setters)
        public int VehicleID
        {
            get { return vehicleID; }
            set { vehicleID = value; }
        }

        public string Make
        {
            get { return make; }
            set { make = value; }
        }

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public decimal DailyRate
        {
            get { return dailyRate; }
            set { dailyRate = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public int PassengerCapacity
        {
            get { return passengerCapacity; }
            set { passengerCapacity = value; }
        }

        public decimal EngineCapacity
        {
            get { return engineCapacity; }
            set { engineCapacity = value; }
        }

        public override string ToString()
        {
            return $"Vehicle ID: {VehicleID}, Make: {Make}, Model: {Model}, Year: {Year}, " +
                   $"Daily Rate: ${DailyRate}, Status: {Status}, Passenger Capacity: {PassengerCapacity}, " +
                   $"Engine Capacity: {EngineCapacity}L";
        }
    }
}