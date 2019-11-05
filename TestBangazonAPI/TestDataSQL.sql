--DELETE FROM OrderProduct;
--DELETE FROM [Order];
--DELETE FROM PaymentType;
--DELETE FROM Product;
--DELETE FROM Customer;
--DELETE FROM ProductType;
--DELETE FROM EmployeeTraining;
--DELETE FROM TrainingProgram;
--DELETE FROM ComputerEmployee;
--DELETE FROM Computer;
--DELETE FROM Employee;
--DELETE FROM Department;


-----Product Types
INSERT INTO ProductType (Name) Values ('Electronics');
INSERT INTO ProductType (Name) Values ('Books');
INSERT INTO ProductType (Name) Values ('Musical Instruments');
INSERT INTO ProductType (Name) Values ('Household Appliances');
INSERT INTO ProductType (Name) Values ('Tools');
INSERT INTO ProductType (Name) Values ('Automobile');

----Customers
INSERT INTO Customer(FirstName, LastName, CreationDate, LastActiveDate) VALUES ('Michael', 'Stiles', '11/5/2019', '11/5/2019');
INSERT INTO Customer(FirstName, LastName, CreationDate, LastActiveDate) VALUES ('Eric', 'Taylor', '01/5/2019', '10/1/2019');
INSERT INTO Customer(FirstName, LastName, CreationDate, LastActiveDate) VALUES ('Kelly', 'Coles', '3/4/2017', '5/25/2019');
INSERT INTO Customer(FirstName, LastName, CreationDate, LastActiveDate) VALUES ('Curtis', 'Crutchfield', '6/17/2019', '9/8/2019');
INSERT INTO Customer(FirstName, LastName, CreationDate, LastActiveDate) VALUES ('Bryan', 'Nilsen', '11/2/2019', '11/3/2019');

----Department
INSERT INTO Department (Name, Budget) VALUES ('Accounting', 70000);
INSERT INTO Department (Name, Budget) VALUES ('HR', 30000);
INSERT INTO Department (Name, Budget) VALUES ('Sales', 20000);
INSERT INTO Department (Name, Budget) VALUES ('Marketing', 670000);
INSERT INTO Department (Name, Budget) VALUES ('Management', 145000);

--Training Program
Insert Into TrainingProgram (StartDate, EndDate, Name, MaxAttendees) VALUES('03/12/2019', '06/14/2019','Customer Satisfaction',25);
Insert Into TrainingProgram (StartDate, EndDate, Name, MaxAttendees) VALUES('06/15/2019', '09/14/2019','Packing Solutions',30);
Insert Into TrainingProgram (StartDate, EndDate, Name, MaxAttendees) VALUES('9/15/2019', '12/14/2019','Typing Lessons',50);
Insert Into TrainingProgram (StartDate, EndDate, Name, MaxAttendees) VALUES('01/15/2020', '4/14/2020','Microsoft Office Training', 100);

--Computers
INSERT INTO Computer (PurchaseDate, Make, Manufacturer) VALUES ('06/10/2019','Insperon', 'Dell');
INSERT INTO Computer (PurchaseDate, Make, Manufacturer) VALUES ('04/19/2018','Yoga','Lenova');
INSERT INTO Computer (PurchaseDate, Make, Manufacturer) VALUES ('01/01/2019','Mac Book Pro', 'Macintosh');
INSERT INTO Computer (PurchaseDate, Make, Manufacturer) VALUES ('06/10/2019','Insperon','Dell');
INSERT INTO Computer (PurchaseDate, Make, Manufacturer) VALUES ('09/01/2019','Mac Book Pro', 'Macintosh');
INSERT INTO Computer (PurchaseDate, Make, Manufacturer) VALUES ('09/01/2019','Mac Book Pro', 'Macintosh');


-------Dependents-----
--Products
INSERT INTO Product (ProductTypeId, CustomerId, Price, Title, Description, Quantity) VALUES (1, 1, 1000, 'Computer', '12" Screen 15GB Ram', 25);
INSERT INTO Product (ProductTypeId, CustomerId, Price, Title, Description, Quantity) VALUES (2, 2, 10, 'The Expanse', 'A Space Odssey', 250);
INSERT INTO Product (ProductTypeId, CustomerId, Price, Title, Description, Quantity) VALUES (3, 3, 10, 'Tuba', 'A Brass', 5);
INSERT INTO Product (ProductTypeId, CustomerId, Price, Title, Description, Quantity) VALUES (4, 4, 5, 'Broom', 'An Item for Sweeping', 2);
INSERT INTO Product (ProductTypeId, CustomerId, Price, Title, Description, Quantity) VALUES (5, 5, 10, 'Wrench', 'A tool for wrenching', 15);
INSERT INTO Product (ProductTypeId, CustomerId, Price, Title, Description, Quantity) VALUES (6, 1, 10, 'Buick', 'Lacrosse the smoothest ride according to Shaq', 30);

--Payment Type
INSERT INTO PaymentType (AcctNumber, Name, CustomerId) Values ('400123321', 'AMEX', 1);
INSERT INTO PaymentType (AcctNumber, Name, CustomerId) Values ('222334126', 'Visa', 1);
INSERT INTO PaymentType (AcctNumber, Name, CustomerId) Values ('8888123346', 'AMEX', 2);
INSERT INTO PaymentType (AcctNumber, Name, CustomerId) Values ('112234326', 'Discover', 3);
INSERT INTO PaymentType (AcctNumber, Name, CustomerId) Values ('96754338', 'AMEX', 4);
INSERT INTO PaymentType (AcctNumber, Name, CustomerId) Values ('7898694849', 'MasterCard', 5);
INSERT INTO PaymentType (AcctNumber, Name, CustomerId) Values ('100500434', 'PayPal', 5);

--Employeees
INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) Values ('George', 'Jefferson', 1, 1);
INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) Values ('Johnny', 'Lawrence', 2, 1);
INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) Values ('Frank', 'Stallone', 3, 1);
INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) Values ('Barry', 'Block', 4, 0);
INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) Values ('Jerry', 'Kidd', 5, 0);
INSERT INTO Employee (FirstName, LastName, DepartmentId, IsSuperVisor) Values ('Jimmy', 'Jimmel', 5, 0);

--Employee Computers
INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate) VALUES (1, 1, '11/5/2019');
INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate) VALUES (2, 2, '10/20/2019');
INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate) VALUES (3, 3, '5/9/2019');

--Employee Training
INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (1, 1);
INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (2, 1);
INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (3, 2);
INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (4, 3);

--Orders
INSERT INTO [Order] (CustomerId, PaymentTypeId) VALUES (1, 1);
INSERT INTO [Order] (CustomerId) VALUES (2);
INSERT INTO [Order] (CustomerId) VALUES (3);

--Order Product
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (1, 1);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (1, 2);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (2, 3);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (2, 4);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES (3, 1);