CREATE VIEW [dbo].[EmployeeInfo]
	AS 
	SELECT 
		n.Id,
		ISNULL(EmployeeName, CONCAT(FirstName, ' ', LastName)) AS EmployeeFullName,
		CONCAT(ZipCode, '_', State, ',', City, '-', Street) AS EmployeeFullAddress,
		CONCAT(CompanyName, '(', Position, ')') AS EmployeeCompanyInfo
	FROM Employee AS n
	LEFT JOIN Address AS a ON n.AddressId=a.Id
	LEFT JOIN Person AS p ON n.PersonId=p.Id
	ORDER BY CompanyName, City OFFSET 0 ROWS