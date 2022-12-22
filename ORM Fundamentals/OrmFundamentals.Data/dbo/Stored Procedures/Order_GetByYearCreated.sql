CREATE PROCEDURE [dbo].[Order_GetByYearCreated]
	@Year int
AS
	SELECT * FROM [dbo].[Order]
	 WHERE Year(CreatedDate) = @Year
RETURN 0
