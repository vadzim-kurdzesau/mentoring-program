CREATE PROCEDURE [dbo].[Order_GetByProductId]
	@ProductId int
AS
	SELECT * FROM [dbo].[Order]
	 WHERE ProductId = @ProductId
RETURN 0
