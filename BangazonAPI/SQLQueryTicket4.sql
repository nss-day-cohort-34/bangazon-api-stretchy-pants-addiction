--SELECT ID, CustomerId, PaymentTypeId FROM [Order];
--SELECT o.Id, o.CustomerId, o.PaymentTypeId FROM [Order] o INNER JOIN PaymentType ON [Order].CustomerId = PaymentType.CustomerId;
--SELECT ID, AcctNumber, CustomerId, Name FROM PaymentType;
--INSERT INTO [Order] (CustomerId, PaymentTypeId) OUTPUT INSERTED.Id VALUES ( 4, 1);
INSERT INTO OrderProduct (OrderId, ProductId) VALUES ( 5, 2);
--INSERT INTO OrderProduct (OrderId, ProductId) OUTPUT INSERTED.Id VALUES ( 5, 1);
--UPDATE [Order] SET CustomerId = 3, PaymentTypeId = 1
--			WHERE Id = 2;
--DELETE FROM OrderProduct WHERE Id = 6;
--SELECT ID, FirstName, LastName, CreationDate, LastActiveDate FROM Customer;