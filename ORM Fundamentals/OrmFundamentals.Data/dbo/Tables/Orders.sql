CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Status] VARCHAR(20) NOT NULL,
	[CreatedDate] DATE NOT NULL,
	[UpdatedDate] DATE,
	[ProductId] INT FOREIGN KEY REFERENCES Products(Id) NOT NULL,
)
