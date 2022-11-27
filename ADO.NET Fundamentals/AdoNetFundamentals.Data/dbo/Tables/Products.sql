CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(100),
	[Weight] REAL,
	[Height] REAL,
	[Width] REAL,
	[Length] REAL,
)
