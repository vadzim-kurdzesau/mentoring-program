CREATE PROCEDURE [dbo].[Orders_GetByYearCreated]
	@Year int
AS
	SELECT * FROM [dbo].[Orders]
	 WHERE Year(CreatedDate) = @Year
RETURN 0