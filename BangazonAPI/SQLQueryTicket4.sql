--SELECT ID, CustomerId, PaymentTypeId FROM [Order];
SELECT o.Id, o.CustomerId, o.PaymentTypeId FROM [Order] o INNER JOIN PaymentType ON [Order].CustomerId = PaymentType.CustomerId;
--SELECT ID, AcctNumber, CustomerId, Name FROM PaymentType;
--INSERT INTO [Order] (CustomerId, PaymentTypeId) OUTPUT INSERTED.Id VALUES ( 3, 1)
--UPDATE [Order] SET CustomerId = 3, PaymentTypeId = 1
--			WHERE Id = 2;
--SELECT ID, FirstName, LastName, CreationDate, LastActiveDate FROM Customer;