CREATE PROCEDURE [dbo].[Orders_GetByProductId]
	@ProductId int
AS
	SELECT * FROM [dbo].[Orders]
	 WHERE ProductId = @ProductId
RETURN 0
