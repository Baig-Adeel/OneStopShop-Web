Use [Test]
Go
--SELECT Cart.CartId,Cart.PaymentAmount,Cart.PaymentType, CartItem.CartItemId,CartItem.Price,CartItem.Quantity, CartItem.ProductId, Products.Name, Products.Price, Products.Description
--FROM Cart JOIN CartItem ON Cart.CartId = CartItem.CartId JOIN Products ON CartItem.ProductId = Products.Id 
--WHERE Cart.CartId = 20003;

--Select * From CartItem JOIN Products ON CartItem.ProductId = Products.Id
--Where CartItem.CartId = 20003;
Select * From Cart Where UserName = 'adeel_baig@hotmail.com';