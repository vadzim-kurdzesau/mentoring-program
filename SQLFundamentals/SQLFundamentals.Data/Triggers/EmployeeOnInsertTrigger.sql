CREATE TRIGGER [dbo].EmployeeOnInsertTrigger
	ON [dbo].[Employee]
	AFTER INSERT
	AS

	DECLARE @CompanyName NVARCHAR(20)
	DECLARE @AddressId INT

	SELECT @CompanyName = CompanyName, @AddressId = AddressId FROM inserted;

	INSERT INTO Company(Name, AddressId)
	VALUES (@CompanyName, @AddressId);
