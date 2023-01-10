CREATE PROCEDURE [dbo].[Orders_GetByMonthCreated]
	@Month int
AS
	SELECT * FROM [dbo].[Orders]
	 WHERE MONTH(CreatedDate) = @Month
RETURN 0
