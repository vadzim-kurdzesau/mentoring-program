﻿CREATE PROCEDURE [dbo].[InsertEmployeeProcedure]
	@EmployeeName NVARCHAR(100) = NULL,
	@FirstName NVARCHAR(50) = NULL,
	@LastName NVARCHAR(50) = NULL,
	@CompanyName NVARCHAR(20),
	@Position NVARCHAR(30) = NULL,
	@Street NVARCHAR(50),
	@City NVARCHAR(20) = NULL,
	@State NVARCHAR(50) = NULL,
	@ZipCode NVARCHAR(50) = NULL
AS
	-- Insert address
	DECLARE @AddressId AS INT
	INSERT INTO [dbo].[Address](City, Street, State, ZipCode)
	VALUES (@City, @Street, @State, @ZipCode);
	
	SELECT @AddressId = SCOPE_IDENTITY();
	
	-- Insert person
	DECLARE @PersonId AS INT
	
	INSERT INTO [dbo].[Person]
	VALUES (@FirstName, @LastName);
		
	SELECT @PersonId = SCOPE_IDENTITY();

	-- Insert employee
	INSERT INTO [dbo].[Employee](AddressId, PersonId, CompanyName, Position, EmployeeName) 
	VALUES (@AddressId, @PersonId, @CompanyName, @Position, @EmployeeName);

RETURN 0
