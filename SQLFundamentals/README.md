# Description

## Task 1:

Create a SQL DB project in VS2019 with the structure of the following tables:

    Person
        Id, int, not NULL, PK
        FirstName, nvarchar(50), not NULL
        LastName, nvarchar(50), not NULL
    Address
        Id, int, not NULL, PK
        Street, nvarchar(50), not NULL
        City, nvarchar(20), NULL
        State, nvarchar(50), NULL
        ZipCode, nvarchar(50), NULL
    Employee
        Id, int, not NULL, PK
        AddressId, int, not NULL, FK (Address.Id)
        PersonId, int, not NULL, FK (Person.Id)
        CompanyName, nvarchar(20), not NULL
        Position, nvarchar(30), NULL
        EmployeeName, nvarchar(100), NULL
    Company
        Id, int, not NULL, PK
        Name, nvarchar(20), not NULL
        AddressId, int, not NULL, FK (Address.Id)

Publish DB into SQL Server with default information (create Script.postdeploy*.sql and fill once all tables with the appropriate data)

## Task 2:

Create ‘EmployeeInfo’ view to show data with the following structure that is sorted by ‘CompanyName, City’ ASC:

    EmployeeId,
    EmployeeFullName (EmployeeName (if not null) or ‘{FirstName} {LastName}’),
    EmployeeFullAddress(‘{ZipCode}_{State}, {City}-{Street}’)
    EmployeeCompanyInfo(‘{CompanyName}({Position})’)

## Task 3:

Create a stored procedure to insert Employee info into DB with the following params:

    EmployeeName(optional)
    FirstName(optional)
    LastName(optional)
    CompanyName(required)
    Position(optional)
    Street(required)
    City(optional)
    State(optional)
    ZipCode(optional)

And the following conditions:

    At least one field (either EmployeeName  or FirstName or LastName) should be not be:
        NULL
        empty string
        contains only ‘space’ symbols
    CompanyName should be truncated if length is more than 20 symbols

## Task 4:

Create a trigger for Employee table on insert new Row that will create a new Company with an Address (The address should be copied from the employee’s address).

## Scoreboard:

1-4 stars – Task 1-3 were implemented.

5 stars – Task 4 task was implemented.