CREATE TABLE [dbo].[Order]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Status] VARCHAR(20) NOT NULL,
	[CreatedDate] DATE NOT NULL,
	[UpdatedDate] DATE,
	[ProductId] INT FOREIGN KEY REFERENCES Product(Id) NOT NULL,
)
