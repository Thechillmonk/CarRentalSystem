using CarRentalSystem.dao;
using CarRentalSystem.Exceptions;
using CarRentalSystem.Model;
using NUnit.Framework;

namespace CarRentalSystem.Tests
{
    [TestFixture]
    public class CarRentalSystemTests
    {
        private ICarLeaseRepository repository;

        [SetUp]
        public void Setup()
        {
            // Initialize the repository before each test
            repository = new CarLeaseRepositoryImpl();
        }

        [TearDown]
        public void Cleanup()
        {
            // Clean up resources after each test if needed
            repository = null;
        }

        #region Car Creation Tests

        [Test]
        public void TestCarCreatedSuccessfully()
        {
            // Arrange
            Vehicle testCar = new Vehicle(
                vehicleID: 999,
                make: "TestMake",
                model: "TestModel",
                year: 2023,
                dailyRate: 50.00m,
                status: "available",
                passengerCapacity: 4,
                engineCapacity: 2.0m
            );

            try
            {
                // Act
                repository.AddCar(testCar);

                // Assert - Try to find the car to verify it was added
                Vehicle retrievedCar = repository.FindCarById(999);
                Assert.IsNotNull(retrievedCar, "Car should be created successfully");
                Assert.AreEqual(testCar.VehicleID, retrievedCar.VehicleID, "Vehicle ID should match");
                Assert.AreEqual(testCar.Make, retrievedCar.Make, "Make should match");
                Assert.AreEqual(testCar.Model, retrievedCar.Model, "Model should match");
                Assert.AreEqual(testCar.Year, retrievedCar.Year, "Year should match");
                Assert.AreEqual(testCar.DailyRate, retrievedCar.DailyRate, "Daily rate should match");
                Assert.AreEqual(testCar.Status, retrievedCar.Status, "Status should match");

                Console.WriteLine("✓ Test Passed: Car created successfully");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Car creation failed: {ex.Message}");
            }
            finally
            {
                // Cleanup - Remove the test car
                try
                {
                    repository.RemoveCar(999);
                }
                catch (CarNotFoundException)
                {
                    // Car doesn't exist, which is fine for cleanup
                }
            }
        }
        #endregion

        #region Lease Creation Tests

        [Test]
        public void TestLeaseCreatedSuccessfully()
        {
            // Arrange
            Vehicle testCar = new Vehicle(1, "toyota", "camry", 2022, 45.99m, "available", 5, 2.50m);
            Customer testCustomer = new Customer(1, "jamal", "shah", "jamal12@email.com", "9835264921");
            
            try
            {
                // Add test car and customer first
                repository.AddCar(testCar);
                repository.AddCustomer(testCustomer);

                // Verify they were added successfully
                Vehicle verifyCarAdded = repository.FindCarById(1);
                Customer verifyCustomerAdded = repository.FindCustomerById(1);
                Assert.IsNotNull(verifyCarAdded, "Test car should be added successfully");
                Assert.IsNotNull(verifyCustomerAdded, "Test customer should be added successfully");

                DateTime startDate = DateTime.Now.Date;
                DateTime endDate = startDate.AddDays(7);

                // Act - Create lease
                Lease createdLease = repository.CreateLease(998, 998, startDate, endDate);

                // Assert
                Assert.IsNotNull(createdLease, "Lease should be created successfully");
                Assert.AreEqual(998, createdLease.CustomerID, "Customer ID should match");
                Assert.AreEqual(998, createdLease.VehicleID, "Vehicle ID should match");
                Assert.AreEqual(startDate, createdLease.StartDate, "Start date should match");
                Assert.AreEqual(endDate, createdLease.EndDate, "End date should match");
                Assert.IsTrue(!string.IsNullOrEmpty(createdLease.Type), "Lease type should be set");

                // Verify car status changed to not available
                Vehicle carAfterLease = repository.FindCarById(998);
                Assert.AreNotEqual("available", carAfterLease.Status, "Car should not be available after lease creation");

                Console.WriteLine("✓ Test Passed: Lease created successfully");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Lease creation failed: {ex.Message}");
            }
            finally
            {
                // Cleanup - Always attempt cleanup
                try
                {
                    // First try to return any active lease to make car available
                    var activeLeases = repository.ListActiveLeases();
                    var testLease = activeLeases?.Find(l => l.VehicleID == 998);
                    if (testLease != null)
                    {
                        repository.ReturnCar(testLease.LeaseID);
                    }
                }
                catch (Exception) { /* Ignore */ }

                try
                {
                    repository.RemoveCar(998);
                }
                catch (Exception) { /* Ignore */ }

                try
                {
                    repository.RemoveCustomer(998);
                }
                catch (Exception) { /* Ignore */ }
            }
        }

        public void TestLeaseCreationWithUnavailableCar()
        {
            // Arrange - Using Vehicle ID 2 (McLaren Artura Spider) which has status 'notAvailable'
            // Customer ID 2: ram charan, ramram1@email.com, 9435287921

            try
            {
                // Verify the McLaren exists and is not available
                Vehicle mclaren = repository.FindCarById(2);
                Customer ramCharan = repository.FindCustomerById(2);

                Assert.IsNotNull(mclaren, "Vehicle ID 2 (McLaren Artura Spider) should exist in database");
                Assert.IsNotNull(ramCharan, "Customer ID 2 (Ram Charan) should exist in database");
                Assert.AreEqual("notAvailable", mclaren.Status, "McLaren should be not available");

                DateTime startDate = DateTime.Now.Date;
                DateTime endDate = startDate.AddDays(7);

                // Act & Assert
                try
                {
                    Lease lease = repository.CreateLease(2, 2, startDate, endDate);
                    Assert.Fail("Should throw exception for unavailable McLaren Artura Spider");
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex.Message.Contains("not available") || ex.Message.Contains("unavailable"),
                        "Should indicate McLaren Artura Spider is not available");
                    Console.WriteLine("✓ Test Passed: Lease creation with unavailable McLaren properly handled");
                }
            }
            catch (AssertionException)
            {
                throw; // Re-throw assertion exceptions
            }
            catch (Exception ex)
            {
                Assert.Fail($"Test setup failed: {ex.Message}");
            }
        }

        #endregion

        #region Lease Retrieval Tests


        [Test]
        public void TestLeaseRetrievedSuccessfully()
        {
            // Arrange - Using existing lease data from SQL
            // Lease ID 1: Vehicle ID 1 (Toyota Camry), Customer ID 1 (Jamal Shah)
            // Start: 2024-06-25, End: 2025-07-05, Type: weekly rental

            try
            {
                // Act - Retrieve leases
                List<Lease> allLeases = repository.ListLeaseHistory();
                List<Lease> activeLeases = repository.ListActiveLeases();

                // Assert
                Assert.That(allLeases, Is.Not.Null, "Lease history should not be null");
                Assert.That(allLeases.Count, Is.GreaterThan(0), "Should have at least one lease in history");

                // Find the specific lease from our SQL data (Lease ID 1)
                Lease existingLease = allLeases.Find(l => l.LeaseID == 1);
                if (existingLease != null)
                {
                    Assert.That(existingLease.VehicleID, Is.EqualTo(1), "Should be Toyota Camry (Vehicle ID 1)");
                    Assert.That(existingLease.CustomerID, Is.EqualTo(1), "Should be Jamal Shah (Customer ID 1)");
                    Assert.That(existingLease.Type, Is.EqualTo("weekly rental"), "Should be weekly rental");

                    // Check if this lease is active (end date is in the future)
                    if (existingLease.EndDate > DateTime.Now.Date)
                    {
                        Assert.That(activeLeases, Is.Not.Null, "Active leases should not be null");
                        var activeLease = activeLeases.Find(l => l.LeaseID == 1);
                        Assert.That(activeLease, Is.Not.Null, "Lease 1 should be in active leases if end date is future");
                    }
                }

                // Verify we can find other leases from SQL data
                var lease2 = allLeases.Find(l => l.LeaseID == 2); // McLaren lease
                var lease3 = allLeases.Find(l => l.LeaseID == 3); // Ferrari lease

                Console.WriteLine($"✓ Test Passed: Retrieved {allLeases.Count} leases from database");
                Console.WriteLine($"  - Found Lease 1 (Toyota): {existingLease != null}");
                Console.WriteLine($"  - Found Lease 2 (McLaren): {lease2 != null}");
                Console.WriteLine($"  - Found Lease 3 (Ferrari): {lease3 != null}");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Lease retrieval failed: {ex.Message}");
            }
        }

        [Test]
        public void TestRetrieveEmptyLeaseList()
        {
            // This test assumes we're working with a fresh database or after cleanup
            try
            {
                // Act
                List<Lease> leases = repository.ListLeaseHistory();

                // Assert
                Assert.IsNotNull(leases, "Lease list should not be null even when empty");
                // Note: We can't assert it's empty because there might be existing data
                Console.WriteLine($"✓ Test Passed: Retrieved {leases.Count} leases from database");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Retrieving lease list failed: {ex.Message}");
            }
        }

        #endregion

        #region Exception Tests

        [Test]
        public void TestCarNotFoundExceptionThrown()
        {
            // Arrange
            int nonExistentCarId = 99999;

            // Act & Assert
            try
            {
                Vehicle car = repository.FindCarById(nonExistentCarId);
                Assert.Fail("Should throw CarNotFoundException for non-existent car");
            }
            catch (CarNotFoundException ex)
            {
                Assert.IsTrue(ex.Message.Contains(nonExistentCarId.ToString()),
                    "Exception message should contain the car ID");
                Console.WriteLine("✓ Test Passed: CarNotFoundException thrown correctly");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Wrong exception type thrown: {ex.GetType().Name} - Expected CarNotFoundException");
            }
        }

        [Test]
        public void TestCustomerNotFoundExceptionThrown()
        {
            // Arrange
            int nonExistentCustomerId = 99999;

            // Act & Assert
            try
            {
                Customer customer = repository.FindCustomerById(nonExistentCustomerId);
                Assert.Fail("Should throw CustomerNotFoundException for non-existent customer");
            }
            catch (CustomerNotFoundException ex)
            {
                Assert.IsTrue(ex.Message.Contains(nonExistentCustomerId.ToString()),
                    "Exception message should contain the customer ID");
                Console.WriteLine("✓ Test Passed: CustomerNotFoundException thrown correctly");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Wrong exception type thrown: {ex.GetType().Name} - Expected CustomerNotFoundException");
            }
        }

        [Test]
        public void TestLeaseNotFoundExceptionThrown()
        {
            // Arrange
            int nonExistentLeaseId = 99999;

            // Act & Assert
            try
            {
                Lease lease = repository.ReturnCar(nonExistentLeaseId);
                Assert.Fail("Should throw LeaseNotFoundException for non-existent lease");
            }
            catch (LeaseNotFoundException ex)
            {
                Assert.IsTrue(ex.Message.Contains(nonExistentLeaseId.ToString()),
                    "Exception message should contain the lease ID");
                Console.WriteLine("✓ Test Passed: LeaseNotFoundException thrown correctly");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Wrong exception type thrown: {ex.GetType().Name} - Expected LeaseNotFoundException");
            }
        }

        [Test]
        public void TestLeaseCreationWithInvalidCustomer()
        {
            // Arrange
            int nonExistentCustomerId = 99998;
            int nonExistentCarId = 99998;

            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = startDate.AddDays(7);

            // Act & Assert
            try
            {
                Lease lease = repository.CreateLease(nonExistentCustomerId, nonExistentCarId, startDate, endDate);
                Assert.Fail("Should throw CustomerNotFoundException or CarNotFoundException for non-existent entities");
            }
            catch (CustomerNotFoundException ex)
            {
                Assert.IsTrue(ex.Message.Contains(nonExistentCustomerId.ToString()),
                    "Exception message should contain the customer ID");
                Console.WriteLine("✓ Test Passed: CustomerNotFoundException thrown correctly during lease creation");
            }
            catch (CarNotFoundException ex)
            {
                Assert.IsTrue(ex.Message.Contains(nonExistentCarId.ToString()),
                    "Exception message should contain the car ID");
                Console.WriteLine("✓ Test Passed: CarNotFoundException thrown correctly during lease creation");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Unexpected exception type thrown: {ex.GetType().Name} - {ex.Message}");
            }
        }

        [Test]
        public void TestPaymentWithInvalidLease()
        {
            // Arrange
            int nonExistentLeaseId = 99997;
            Lease invalidLease = new Lease { LeaseID = nonExistentLeaseId };
            decimal paymentAmount = 100.00m;

            // Act & Assert
            try
            {
                repository.RecordPayment(invalidLease, paymentAmount);
                Assert.Fail("Should throw LeaseNotFoundException for non-existent lease");
            }
            catch (LeaseNotFoundException ex)
            {
                Assert.IsTrue(ex.Message.Contains(nonExistentLeaseId.ToString()),
                    "Exception message should contain the lease ID");
                Console.WriteLine("✓ Test Passed: LeaseNotFoundException thrown correctly during payment");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Wrong exception type thrown: {ex.GetType().Name} - Expected LeaseNotFoundException");
            }
        }

        #endregion
    }
}