using CarRentalSystem.Exceptions;
using CarRentalSystem.Model;
using CarRentalSystem.Ultility;
using System.Data;
using System.Data.SqlClient;

namespace CarRentalSystem.dao
{
    public class CarLeaseRepositoryImpl : ICarLeaseRepository
    {
        private SqlConnection connection;
        private SqlCommand command;

        public CarLeaseRepositoryImpl()
        {
            connection = DBConnUtil.GetConnection();
            command = new SqlCommand();
        }

        // Car Management Methods
        public void AddCar(Vehicle car)
        {
            try
            {
                command.CommandText = @"INSERT INTO Vehicle (vehicleID, make, model, yearr, dailyRate, statuss, passengerCapacity, engineCapacity) 
                                      VALUES (@vehicleID, @make, @model, @year, @dailyRate, @status, @passengerCapacity, @engineCapacity)";
                command.Connection = connection;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@vehicleID", car.VehicleID);
                command.Parameters.AddWithValue("@make", car.Make);
                command.Parameters.AddWithValue("@model", car.Model);
                command.Parameters.AddWithValue("@year", car.Year);
                command.Parameters.AddWithValue("@dailyRate", car.DailyRate);
                command.Parameters.AddWithValue("@status", car.Status);
                command.Parameters.AddWithValue("@passengerCapacity", car.PassengerCapacity);
                command.Parameters.AddWithValue("@engineCapacity", car.EngineCapacity);

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding car: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void RemoveCar(int carID)
        {
            try
            {
                // First check if car exists
                Vehicle car = FindCarById(carID);

                command.CommandText = "DELETE FROM Vehicle WHERE vehicleID = @vehicleID";
                command.Connection = connection;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@vehicleID", carID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new CarNotFoundException($"Car with ID {carID} not found");
                }
            }
            catch (CarNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing car: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public List<Vehicle> ListAvailableCars()
        {
            List<Vehicle> availableCars = new List<Vehicle>();

            try
            {
                command.CommandText = "SELECT * FROM Vehicle WHERE statuss = 'available'";
                command.Connection = connection;
                command.Parameters.Clear();

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Vehicle car = new Vehicle(
                        (int)reader["vehicleID"],
                        reader["make"].ToString(),
                        reader["model"].ToString(),
                        (int)reader["yearr"],
                        (decimal)reader["dailyRate"],
                        reader["statuss"].ToString(),
                        (int)reader["passengerCapacity"],
                        (decimal)reader["engineCapacity"]
                    );
                    availableCars.Add(car);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error listing available cars: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return availableCars;
        }

        public List<Vehicle> ListRentedCars()
        {
            List<Vehicle> rentedCars = new List<Vehicle>();

            try
            {
                command.CommandText = "SELECT * FROM Vehicle WHERE statuss = 'notAvailable'";
                command.Connection = connection;
                command.Parameters.Clear();

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Vehicle car = new Vehicle(
                        (int)reader["vehicleID"],
                        reader["make"].ToString(),
                        reader["model"].ToString(),
                        (int)reader["yearr"],
                        (decimal)reader["dailyRate"],
                        reader["statuss"].ToString(),
                        (int)reader["passengerCapacity"],
                        (decimal)reader["engineCapacity"]
                    );
                    rentedCars.Add(car);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error listing rented cars: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return rentedCars;
        }

        public Vehicle FindCarById(int carID)
        {
            Vehicle car = null;

            try
            {
                command.CommandText = "SELECT * FROM Vehicle WHERE vehicleID = @vehicleID";
                command.Connection = connection;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@vehicleID", carID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    car = new Vehicle(
                        (int)reader["vehicleID"],
                        reader["make"].ToString(),
                        reader["model"].ToString(),
                        (int)reader["yearr"],
                        (decimal)reader["dailyRate"],
                        reader["statuss"].ToString(),
                        (int)reader["passengerCapacity"],
                        (decimal)reader["engineCapacity"]
                    );
                }
                reader.Close();

                if (car == null)
                {
                    throw new CarNotFoundException($"Car with ID {carID} not found");
                }
            }
            catch (CarNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error finding car: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return car;
        }

        // Customer Management Methods
        public void AddCustomer(Customer customer)
        {
            try
            {
                command.CommandText = @"INSERT INTO Customer (customerID, firstName, lastName, email, phoneNumber) 
                                      VALUES (@customerID, @firstName, @lastName, @email, @phoneNumber)";
                command.Connection = connection;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@customerID", customer.CustomerID);
                command.Parameters.AddWithValue("@firstName", customer.FirstName);
                command.Parameters.AddWithValue("@lastName", customer.LastName);
                command.Parameters.AddWithValue("@email", customer.Email);
                command.Parameters.AddWithValue("@phoneNumber", customer.PhoneNumber);

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding customer: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void RemoveCustomer(int customerID)
        {
            try
            {
                // First check if customer exists
                Customer customer = FindCustomerById(customerID);

                command.CommandText = "DELETE FROM Customer WHERE customerID = @customerID";
                command.Connection = connection;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@customerID", customerID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new CustomerNotFoundException($"Customer with ID {customerID} not found");
                }
            }
            catch (CustomerNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing customer: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public List<Customer> ListCustomers()
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                command.CommandText = "SELECT * FROM Customer";
                command.Connection = connection;
                command.Parameters.Clear();

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Customer customer = new Customer(
                        (int)reader["customerID"],
                        reader["firstName"].ToString(),
                        reader["lastName"].ToString(),
                        reader["email"].ToString(),
                        reader["phoneNumber"].ToString()
                    );
                    customers.Add(customer);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error listing customers: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return customers;
        }

        public Customer FindCustomerById(int customerID)
        {
            Customer customer = null;

            try
            {
                command.CommandText = "SELECT * FROM Customer WHERE customerID = @customerID";
                command.Connection = connection;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@customerID", customerID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    customer = new Customer(
                        (int)reader["customerID"],
                        reader["firstName"].ToString(),
                        reader["lastName"].ToString(),
                        reader["email"].ToString(),
                        reader["phoneNumber"].ToString()
                    );
                }
                reader.Close();

                if (customer == null)
                {
                    throw new CustomerNotFoundException($"Customer with ID {customerID} not found");
                }
            }
            catch (CustomerNotFoundException)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return customer;
        }

        // Lease Management Methods
        public Lease CreateLease(int customerID, int carID, DateTime startDate, DateTime endDate)
        {
            Lease lease = null;

            try
            {
                // Verify customer and car exist
                Customer customer = FindCustomerById(customerID);
                Vehicle car = FindCarById(carID);

                // Check if car is available
                if (car.Status != "available")
                {
                    throw new Exception($"Car with ID {carID} is not available for lease");
                }

                // Calculate total amount based on duration and daily rate
                TimeSpan duration = endDate - startDate;
                int totalDays = duration.Days;
                if (totalDays <= 0)
                {
                    throw new Exception("End date must be after start date");
                }

                decimal totalAmount = totalDays * car.DailyRate; // CALCULATE TOTAL AMOUNT

                // Generate new lease ID
                int leaseID = GenerateLeaseID();

                // Determine lease type based on duration
                string leaseType = duration.Days >= 30 ? "monthly rental" : "weekly rental";

                connection.Open();

                // Begin transaction to ensure both operations complete
                SqlTransaction transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    // Create lease record - UPDATED to include totalAmount
                    command.CommandText = @"INSERT INTO Lease (leaseID, vehicleID, customerID, startdate, enddate, typee, totalAmount) 
                                  VALUES (@leaseID, @vehicleID, @customerID, @startDate, @endDate, @type, @totalAmount)";

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@leaseID", leaseID);
                    command.Parameters.AddWithValue("@vehicleID", carID);
                    command.Parameters.AddWithValue("@customerID", customerID);
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@endDate", endDate);
                    command.Parameters.AddWithValue("@type", leaseType);
                    command.Parameters.AddWithValue("@totalAmount", totalAmount); // NEW PARAMETER

                    command.ExecuteNonQuery();

                    // Update car status to not available
                    command.CommandText = "UPDATE Vehicle SET statuss = 'notAvailable' WHERE vehicleID = @vehicleID";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@vehicleID", carID);

                    command.ExecuteNonQuery();

                    // Commit transaction
                    transaction.Commit();

                    // Create lease object with calculated total amount
                    lease = new Lease(leaseID, carID, customerID, startDate, endDate, leaseType, totalAmount);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            catch (CustomerNotFoundException)
            {
                throw;
            }
            catch (CarNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating lease: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return lease;
        }

        public Lease ReturnCar(int leaseID)
        {
            Lease lease = null;

            try
            {
                connection.Open();

                // Find the lease - UPDATED to include totalAmount
                command.CommandText = "SELECT * FROM Lease WHERE leaseID = @leaseID";
                command.Connection = connection;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@leaseID", leaseID);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    lease = new Lease(
                        (int)reader["leaseID"],
                        (int)reader["vehicleID"],
                        (int)reader["customerID"],
                        (DateTime)reader["startdate"],
                        (DateTime)reader["enddate"],
                        reader["typee"].ToString(),
                        reader["totalAmount"] != DBNull.Value ? (decimal)reader["totalAmount"] : 0 // UPDATED
                    );
                }
                reader.Close();

                if (lease == null)
                {
                    throw new LeaseNotFoundException($"Lease with ID {leaseID} not found");
                }

                // Update car status to available
                command.CommandText = "UPDATE Vehicle SET statuss = 'available' WHERE vehicleID = @vehicleID";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@vehicleID", lease.VehicleID);

                command.ExecuteNonQuery();
            }
            catch (LeaseNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error returning car: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return lease;
        }
        public List<Lease> ListActiveLeases()
        {
            List<Lease> activeLeases = new List<Lease>();

            try
            {
                // Get all leases where the end date is in the future
                // and the associated vehicle is currently not available - UPDATED
                command.CommandText = @"SELECT l.* FROM Lease l 
                              INNER JOIN Vehicle v ON l.vehicleID = v.vehicleID 
                              WHERE v.statuss = 'notAvailable' AND l.enddate >= @currentDate";
                command.Connection = connection;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@currentDate", DateTime.Now.Date);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Lease lease = new Lease(
                        (int)reader["leaseID"],
                        (int)reader["vehicleID"],
                        (int)reader["customerID"],
                        (DateTime)reader["startdate"],
                        (DateTime)reader["enddate"],
                        reader["typee"].ToString(),
                        reader["totalAmount"] != DBNull.Value ? (decimal)reader["totalAmount"] : 0 // UPDATED
                    );
                    activeLeases.Add(lease);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error listing active leases: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return activeLeases;
        }


        public List<Lease> ListLeaseHistory()
        {
            List<Lease> leaseHistory = new List<Lease>();

            try
            {
                command.CommandText = "SELECT * FROM Lease ORDER BY startdate DESC"; // UPDATED
                command.Connection = connection;
                command.Parameters.Clear();

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Lease lease = new Lease(
                        (int)reader["leaseID"],
                        (int)reader["vehicleID"],
                        (int)reader["customerID"],
                        (DateTime)reader["startdate"],
                        (DateTime)reader["enddate"],
                        reader["typee"].ToString(),
                        reader["totalAmount"] != DBNull.Value ? (decimal)reader["totalAmount"] : 0 // UPDATED
                    );
                    leaseHistory.Add(lease);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error listing lease history: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return leaseHistory;
        }
        // Payment Handling Methods - COMPLETE IMPLEMENTATION
        public void RecordPayment(Lease lease, decimal amount)
        {
            try
            {
                connection.Open();

                // Verify lease exists
                command.CommandText = "SELECT COUNT(*) FROM Lease WHERE leaseID = @leaseID";
                command.Connection = connection;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@leaseID", lease.LeaseID);

                int leaseExists = (int)command.ExecuteScalar();

                if (leaseExists == 0)
                {
                    throw new LeaseNotFoundException($"Lease with ID {lease.LeaseID} not found");
                }

                // Generate payment ID
                int paymentID = GeneratePaymentID();

                // Record payment
                command.CommandText = @"INSERT INTO Payment (paymentID, leaseID, paymentdate, amount) 
                                      VALUES (@paymentID, @leaseID, @paymentDate, @amount)";

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@paymentID", paymentID);
                command.Parameters.AddWithValue("@leaseID", lease.LeaseID);
                command.Parameters.AddWithValue("@paymentDate", DateTime.Now);
                command.Parameters.AddWithValue("@amount", amount);

                command.ExecuteNonQuery();
            }
            catch (LeaseNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error recording payment: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        // NEW METHOD: Get Payment History for a Customer
        public List<Payment> GetPaymentHistory(int customerID)
        {
            List<Payment> paymentHistory = new List<Payment>();

            try
            {
                // Verify customer exists
                Customer customer = FindCustomerById(customerID);

                command.CommandText = @"SELECT p.* FROM Payment p 
                                      INNER JOIN Lease l ON p.leaseID = l.leaseID 
                                      WHERE l.customerID = @customerID 
                                      ORDER BY p.paymentdate DESC";
                command.Connection = connection;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@customerID", customerID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Payment payment = new Payment(
                        (int)reader["paymentID"],
                        (int)reader["leaseID"],
                        (DateTime)reader["paymentdate"],
                        (decimal)reader["amount"]
                    );
                    paymentHistory.Add(payment);
                }
                reader.Close();
            }
            catch (CustomerNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving payment history: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return paymentHistory;
        }

        // NEW METHOD: Calculate Total Revenue from all Payments
        public decimal CalculateTotalRevenue()
        {
            decimal totalRevenue = 0;

            try
            {
                command.CommandText = "SELECT ISNULL(SUM(amount), 0) as TotalRevenue FROM Payment";
                command.Connection = connection;
                command.Parameters.Clear();

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    totalRevenue = (decimal)result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error calculating total revenue: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return totalRevenue;
        }

        // Helper Methods
        private int GenerateLeaseID()
        {
            try
            {
                command.CommandText = "SELECT ISNULL(MAX(leaseID), 0) + 1 FROM Lease";
                command.Connection = connection;
                command.Parameters.Clear();

                // Don't open connection here if it's already open from parent method
                bool wasOpen = connection.State == ConnectionState.Open;
                if (!wasOpen)
                    connection.Open();

                int result = (int)command.ExecuteScalar();

                if (!wasOpen)
                    connection.Close();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating lease ID: {ex.Message}");
            }
        }

        private int GeneratePaymentID()
        {
            try
            {
                command.CommandText = "SELECT ISNULL(MAX(paymentID), 0) + 1 FROM Payment";
                command.Connection = connection;
                command.Parameters.Clear();

                // Don't open connection here if it's already open from parent method
                bool wasOpen = connection.State == ConnectionState.Open;
                if (!wasOpen)
                    connection.Open();

                int result = (int)command.ExecuteScalar();

                if (!wasOpen)
                    connection.Close();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating payment ID: {ex.Message}");
            }
        }
    }
}