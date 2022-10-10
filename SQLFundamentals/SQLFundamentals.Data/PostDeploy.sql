INSERT INTO Person(FirstName, LastName) 
     VALUES ('Vadzim', 'Kurdzesau'),
            ('TestUserName', 'TestUserLastName'),
            ('TestUserName1', 'TestUserLastName1');

INSERT INTO Address(Street, City, State, ZipCode)
     VALUES ('TestStreet1', 'TestCity1', 'TestState1', '111222'),
            ('TestStreet2', 'TestCity1', 'TestState1', '111333'),
            ('TestStreet3', 'TestCity2', 'TestState2', '333222'),
            ('TestStreet4', 'TestCity3', 'TestState1', '123456'),
            ('TestStreet5', 'TestTown2', 'TestState2', '333444'),
            ('TestStreet6', 'TestTown1', 'TestCountry1', '333-44');

INSERT INTO Employee(AddressId, PersonId, CompanyName, Position, EmployeeName)
     VALUES (1, 1, 'EPAM', 'Developer', 'Vadzim'),
            (2, 2, 'TestCompany1', 'TestPosition1', 'TestUserName'),
            (3, 3, 'TestCompany2', 'TestPosition2', 'TestUserName1');

INSERT INTO Company(Name, AddressId)
     VALUES ('EPAM', 4),
            ('TestCompany1', 5),
            ('TestCompany2', 6);