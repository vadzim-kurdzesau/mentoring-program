CREATE PROCEDURE [dbo].[Order_GetByMonthCreated]
	@Month int
AS
	SELECT * FROM [dbo].[Order]
	 WHERE MONTH(CreatedDate) = @Month
RETURN 0
