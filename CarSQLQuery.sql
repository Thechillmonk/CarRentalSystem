create database CarRentalSystem

-- create Vehicle Table
create table Vehicle (
    vehicleID int primary key,
    make varchar(50),
    model varchar(50),
    yearr int,
    dailyRate decimal(10, 2),
    statuss varchar(20),
    passengerCapacity int,
    engineCapacity decimal(5, 2))

-- Create Customer Table
create table Customer (
    customerID int primary key,
    firstName varchar(50),
    lastName varchar(50),
    email varchar(100) unique not null,
    phoneNumber varchar(15));

-- Create Lease Table
create table Lease (
    leaseID int primary key,
    vehicleID int foreign key references Vehicle(vehicleID),
    customerID int foreign key references Customer(customerID),
    startdate date,
    enddate date,
    typee varchar(30))

-- Create Payment Table
create table Payment (
    paymentID int primary key,
    leaseID int foreign key references Lease(leaseID),
    paymentdate date,
    amount decimal(10, 2))

    use CarRentalSystem

  -- sample data for vehicle table
insert into vehicle (vehicleid, make, model, yearr, dailyrate, statuss, passengercapacity, enginecapacity)
values 
      (1, 'toyota', 'camry', 2022, 45.99, 'available', 5, 2.50),
      (2, 'mclaren', 'artura spider', 2023, 46.99, 'notAvailable', 2, 3.50),
      (3, 'ferrarri', 'california', 2024, 47.99, 'available', 3, 2.70)



-- sample data for customer table
insert into customer (customerid, firstname, lastname, email, phonenumber)
values 
     (1, 'jamal', 'shah', 'jamal12@email.com', '9835264921'),
     (2, 'ram', 'charan', 'ramram1@email.com', '9435287921'),
     (3, 'micheal', 'jordan', 'mjordan22@email.com', '9211264926')

-- sample data for lease table
insert into lease (leaseid, vehicleid, customerid, startdate, enddate, typee)
values 
     (1, 1, 1, '2024-06-25', '2025-07-05', 'weekly rental'),
     (2, 2, 1, '2024-05-02', '2024-05-08', 'weekly rental'),
     (3, 3, 2, '2024-08-03', '2024-08-09', 'weekly rental')

-- sample data for payment table
insert into payment (paymentid, leaseid, paymentdate, amount)
values 
     (1, 1, '2024-06-01', 321.93),
     (2, 2, '2024-05-02', 350.93),
     (3, 1, '2024-08-03', 410.93)

    

select * from Lease

ALTER TABLE Lease ADD totalAmount decimal(10, 2) DEFAULT 0;
UPDATE Lease SET totalAmount = 0 WHERE totalAmount IS NULL;