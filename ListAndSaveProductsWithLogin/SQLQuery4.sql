Use [Test]
Go
CREATE PROCEDURE sp_InsertCartItem

  
    @CartId     BIGINT ,         
    @ProductId  INT ,            
    @Price      DECIMAL (18, 2), 
    @Quantity   INT             
    AS
    INSERT INTO [dbo.CartItem] ([CartId], [ProductId], [Price], [Quantity])
    VALUES (@CartId, @ProductId, @Price, @Quantity)
    Go