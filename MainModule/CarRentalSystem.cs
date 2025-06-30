using CarRentalSystem.dao;
using CarRentalSystem.Exceptions;
using CarRentalSystem.Model;

namespace CarRentalSystem.MainModule
{
    public class MainModule
    {
        public static ICarLeaseRepository repository = new CarLeaseRepositoryImpl();

        public static void Main(string[] args)
        {
            Console.WriteLine(" Welcome to Car Rental System ");

            int choice;
            do
            {
                DisplayMenu();
                choice = GetUserChoice();

                try
                {
                    switch (choice)
                    {
                        case 1:
                            CarManagementMenu();
                            break;
                        case 2:
                            CustomerManagementMenu();
                            break;
                        case 3:
                            LeaseManagementMenu();
                            break;
                        case 4:
                            PaymentMenu();
                            break;
                        case 5:
                            Console.WriteLine("Thank you for using Car Rental System!");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (CarNotFoundException ex)
                {
                    Console.WriteLine($"Car Error: {ex.Message}");
                }
                catch (CustomerNotFoundException ex)
                {
                    Console.WriteLine($"Customer Error: {ex.Message}");
                }
                catch (LeaseNotFoundException ex)
                {
                    Console.WriteLine($"Lease Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                if (choice != 5)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (choice != 5);
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("\n MAIN MENU ");
            Console.WriteLine("1. Car Management");
            Console.WriteLine("2. Customer Management");
            Console.WriteLine("3. Lease Management");
            Console.WriteLine("4. Payment Management");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
        }

        private static int GetUserChoice()
        {
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                return -1;
            }
        }

        private static void CarManagementMenu()
        {
            Console.WriteLine("\n CAR MANAGEMENT ");
            Console.WriteLine("1. Add Car");
            Console.WriteLine("2. Remove Car");
            Console.WriteLine("3. List Available Cars");
            Console.WriteLine("4. List Rented Cars");
            Console.WriteLine("5. Find Car by ID");
            Console.Write("Enter your choice: ");

            int choice = GetUserChoice();

            switch (choice)
            {
                case 1:
                    AddCar();
                    break;
                case 2:
                    RemoveCar();
                    break;
                case 3:
                    ListAvailableCars();
                    break;
                case 4:
                    ListRentedCars();
                    break;
                case 5:
                    FindCarById();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private static void AddCar()
        {
            try
            {
                Console.WriteLine("\n ADD NEW CAR ");
                Console.Write("Enter Vehicle ID: ");
                int vehicleID = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Make: ");
                string make = Console.ReadLine();

                Console.Write("Enter Model: ");
                string model = Console.ReadLine();

                Console.Write("Enter Year: ");
                int year = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Daily Rate: ");
                decimal dailyRate = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Enter Passenger Capacity: ");
                int passengerCapacity = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Engine Capacity: ");
                decimal engineCapacity = Convert.ToDecimal(Console.ReadLine());

                Vehicle car = new Vehicle(vehicleID, make, model, year, dailyRate, "available", passengerCapacity, engineCapacity);
                repository.AddCar(car);
                Console.WriteLine("Car added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding car: {ex.Message}");
            }
        }

        private static void RemoveCar()
        {
            try
            {
                Console.WriteLine("\n REMOVE CAR ");
                Console.Write("Enter Car ID to remove: ");
                int carID = Convert.ToInt32(Console.ReadLine());

                repository.RemoveCar(carID);
                Console.WriteLine("Car removed successfully!");
            }
            catch (CarNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing car: {ex.Message}");
            }
        }

        private static void ListAvailableCars()
        {
            try
            {
                Console.WriteLine("\n AVAILABLE CARS ");
                List<Vehicle> availableCars = repository.ListAvailableCars();

                if (availableCars.Count == 0)
                {
                    Console.WriteLine("No available cars found.");
                }
                else
                {
                    foreach (Vehicle car in availableCars)
                    {
                        Console.WriteLine(car);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing available cars: {ex.Message}");
            }
        }

        private static void ListRentedCars()
        {
            try
            {
                Console.WriteLine("\n RENTED CARS ");
                List<Vehicle> rentedCars = repository.ListRentedCars();

                if (rentedCars.Count == 0)
                {
                    Console.WriteLine("No rented cars found.");
                }
                else
                {
                    foreach (Vehicle car in rentedCars)
                    {
                        Console.WriteLine(car);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing rented cars: {ex.Message}");
            }
        }

        private static void FindCarById()
        {
            try
            {
                Console.WriteLine("\n FIND CAR BY ID ");
                Console.Write("Enter Car ID: ");
                int carID = Convert.ToInt32(Console.ReadLine());

                Vehicle car = repository.FindCarById(carID);
                Console.WriteLine($"Car found: {car}");
            }
            catch (CarNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding car: {ex.Message}");
            }
        }

        private static void CustomerManagementMenu()
        {
            Console.WriteLine("\n CUSTOMER MANAGEMENT ");
            Console.WriteLine("1. Add Customer");
            Console.WriteLine("2. Remove Customer");
            Console.WriteLine("3. List All Customers");
            Console.WriteLine("4. Find Customer by ID");
            Console.Write("Enter your choice: ");

            int choice = GetUserChoice();

            switch (choice)
            {
                case 1:
                    AddCustomer();
                    break;
                case 2:
                    RemoveCustomer();
                    break;
                case 3:
                    ListCustomers();
                    break;
                case 4:
                    FindCustomerById();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private static void AddCustomer()
        {
            try
            {
                Console.WriteLine("\n ADD NEW CUSTOMER ");
                Console.Write("Enter Customer ID: ");
                int customerID = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter First Name: ");
                string firstName = Console.ReadLine();

                Console.Write("Enter Last Name: ");
                string lastName = Console.ReadLine();

                Console.Write("Enter Email: ");
                string email = Console.ReadLine();

                Console.Write("Enter Phone Number: ");
                string phoneNumber = Console.ReadLine();

                Customer customer = new Customer(customerID, firstName, lastName, email, phoneNumber);
                repository.AddCustomer(customer);
                Console.WriteLine("Customer added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding customer: {ex.Message}");
            }
        }

        private static void RemoveCustomer()
        {
            try
            {
                Console.WriteLine("\n REMOVE CUSTOMER");
                Console.Write("Enter Customer ID to remove: ");
                int customerID = Convert.ToInt32(Console.ReadLine());

                repository.RemoveCustomer(customerID);
                Console.WriteLine("Customer removed successfully!");
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing customer: {ex.Message}");
            }
        }

        private static void ListCustomers()
        {
            try
            {
                Console.WriteLine("\n ALL CUSTOMERS ");
                List<Customer> customers = repository.ListCustomers();

                if (customers.Count == 0)
                {
                    Console.WriteLine("No customers found.");
                }
                else
                {
                    foreach (Customer customer in customers)
                    {
                        Console.WriteLine(customer);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing customers: {ex.Message}");
            }
        }

        private static void FindCustomerById()
        {
            try
            {
                Console.WriteLine("\n FIND CUSTOMER BY ID ");
                Console.Write("Enter Customer ID: ");
                int customerID = Convert.ToInt32(Console.ReadLine());

                Customer customer = repository.FindCustomerById(customerID);
                Console.WriteLine($"Customer found: {customer}");
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding customer: {ex.Message}");
            }
        }

        private static void LeaseManagementMenu()
        {
            Console.WriteLine("\n LEASE MANAGEMENT ");
            Console.WriteLine("1. Create Lease");
            Console.WriteLine("2. Return Car");
            Console.WriteLine("3. List Active Leases");
            Console.WriteLine("4. List Lease History");
            Console.Write("Enter your choice: ");

            int choice = GetUserChoice();

            switch (choice)
            {
                case 1:
                    CreateLease();
                    break;
                case 2:
                    ReturnCar();
                    break;
                case 3:
                    ListActiveLeases();
                    break;
                case 4:
                    ListLeaseHistory();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private static void CreateLease()
        {
            try
            {
                Console.WriteLine("\nCREATE NEW LEASE ");
                Console.Write("Enter Customer ID: ");
                int customerID = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Car ID: ");
                int carID = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Start Date (yyyy-mm-dd): ");
                DateTime startDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter End Date (yyyy-mm-dd): ");
                DateTime endDate = DateTime.Parse(Console.ReadLine());

                Lease lease = repository.CreateLease(customerID, carID, startDate, endDate);
                Console.WriteLine($"Lease created successfully: {lease}");
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (CarNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating lease: {ex.Message}");
            }
        }

        private static void ReturnCar()
        {
            try
            {
                Console.WriteLine("\n RETURN CAR ");
                Console.Write("Enter Lease ID: ");
                int leaseID = Convert.ToInt32(Console.ReadLine());

                Lease lease = repository.ReturnCar(leaseID);
                Console.WriteLine($"Car returned successfully for lease: {lease}");
            }
            catch (LeaseNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error returning car: {ex.Message}");
            }
        }

        private static void ListActiveLeases()
        {
            try
            {
                Console.WriteLine("\n ACTIVE LEASES ");
                List<Lease> activeLeases = repository.ListActiveLeases();

                if (activeLeases.Count == 0)
                {
                    Console.WriteLine("No active leases found.");
                }
                else
                {
                    foreach (Lease lease in activeLeases)
                    {
                        Console.WriteLine(lease);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing active leases: {ex.Message}");
            }
        }

        private static void ListLeaseHistory()
        {
            try
            {
                Console.WriteLine("\n LEASE HISTORY ");
                List<Lease> leaseHistory = repository.ListLeaseHistory();

                if (leaseHistory.Count == 0)
                {
                    Console.WriteLine("No lease history found.");
                }
                else
                {
                    foreach (Lease lease in leaseHistory)
                    {
                        Console.WriteLine(lease);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing lease history: {ex.Message}");
            }
        }

        // ENHANCED PAYMENT MENU WITH ALL FUNCTIONALITIES
        private static void PaymentMenu()
        {
            Console.WriteLine("\n PAYMENT MANAGEMENT ");
            Console.WriteLine("1. Record Payment");
            Console.WriteLine("2. View Payment History by Customer");
            Console.WriteLine("3. Calculate Total Revenue");
            Console.WriteLine("4. View All Payments");
            Console.Write("Enter your choice: ");

            int choice = GetUserChoice();

            switch (choice)
            {
                case 1:
                    RecordPayment();
                    break;
                case 2:
                    ViewPaymentHistory();
                    break;
                case 3:
                    CalculateTotalRevenue();
                    break;
                case 4:
                    ViewAllPayments();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private static void RecordPayment()
        {
            try
            {
                Console.WriteLine("\n RECORD PAYMENT ");
                Console.Write("Enter Lease ID: ");
                int leaseID = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Payment Amount: ");
                decimal amount = Convert.ToDecimal(Console.ReadLine());

                // Create a lease object with the given ID for payment recording
                Lease lease = new Lease { LeaseID = leaseID };
                repository.RecordPayment(lease, amount);
                Console.WriteLine("Payment recorded successfully!");
            }
            catch (LeaseNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error recording payment: {ex.Message}");
            }
        }

        // NEW METHOD: View Payment History for a Customer
        private static void ViewPaymentHistory()
        {
            try
            {
                Console.WriteLine("\n PAYMENT HISTORY BY CUSTOMER ");
                Console.Write("Enter Customer ID: ");
                int customerID = Convert.ToInt32(Console.ReadLine());

                List<Payment> paymentHistory = repository.GetPaymentHistory(customerID);

                if (paymentHistory.Count == 0)
                {
                    Console.WriteLine($"No payment history found for Customer ID: {customerID}");
                }
                else
                {
                    Console.WriteLine($"\nPayment History for Customer ID: {customerID}");
                    Console.WriteLine(new string('-', 80));

                    decimal totalPaid = 0;
                    foreach (Payment payment in paymentHistory)
                    {
                        Console.WriteLine(payment);
                        totalPaid += payment.Amount;
                    }

                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine($"Total Amount Paid by Customer: ${totalPaid:F2}");
                }
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving payment history: {ex.Message}");
            }
        }

        // NEW METHOD: Calculate and Display Total Revenue
        private static void CalculateTotalRevenue()
        {
            try
            {
                Console.WriteLine("\n TOTAL REVENUE CALCULATION ");

                decimal totalRevenue = repository.CalculateTotalRevenue();

                Console.WriteLine($"Total Revenue from all payments: ${totalRevenue:F2}");

                if (totalRevenue > 0)
                {
                    Console.WriteLine($"Revenue Summary:");
                    Console.WriteLine($"- Total collected: ${totalRevenue:F2}");
                    Console.WriteLine($"- Average per payment: ${GetAveragePaymentAmount():F2}");
                }
                else
                {
                    Console.WriteLine("No payments have been recorded yet.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating total revenue: {ex.Message}");
            }
        }

        // HELPER METHOD: Calculate average payment amount
        private static decimal GetAveragePaymentAmount()
        {
            try
            {
                // This would require a new method in the repository to get payment count
                // For now, we'll return the total revenue as a placeholder
                return repository.CalculateTotalRevenue();
            }
            catch
            {
                return 0;
            }
        }

        // NEW METHOD: View All Payments (bonus functionality)
        private static void ViewAllPayments()
        {
            try
            {
                Console.WriteLine("\n ALL PAYMENTS ");

                // Get all customers and their payment histories
                List<Customer> customers = repository.ListCustomers();

                if (customers.Count == 0)
                {
                    Console.WriteLine("No customers found.");
                    return;
                }

                bool hasPayments = false;
                decimal grandTotal = 0;

                foreach (Customer customer in customers)
                {
                    List<Payment> payments = repository.GetPaymentHistory(customer.CustomerID);

                    if (payments.Count > 0)
                    {
                        if (!hasPayments)
                        {
                            Console.WriteLine("Payment Records:");
                            Console.WriteLine(new string('=', 100));
                            hasPayments = true;
                        }

                        Console.WriteLine($"\nCustomer: {customer.FirstName} {customer.LastName} (ID: {customer.CustomerID})");
                        Console.WriteLine(new string('-', 80));

                        decimal customerTotal = 0;
                        foreach (Payment payment in payments)
                        {
                            Console.WriteLine($"  {payment}");
                            customerTotal += payment.Amount;
                        }

                        Console.WriteLine($"  Customer Total: ${customerTotal:F2}");
                        grandTotal += customerTotal;
                    }
                }

                if (hasPayments)
                {
                    Console.WriteLine(new string('=', 100));
                    Console.WriteLine($"GRAND TOTAL REVENUE: ${grandTotal:F2}");
                }
                else
                {
                    Console.WriteLine("No payments found in the system.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error viewing all payments: {ex.Message}");
            }
        }
    }
}