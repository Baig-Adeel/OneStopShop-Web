using ListAndSaveProducts.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ListAndSaveProducts.Services
{
    public class CheckoutDAO : ICheckoutDataServices
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                //MultipleActiveResultSets=true";

        public List<CartModel> GetAllCarts()
        {
            List<CartModel> cartlList = new List<CartModel>();
            

            string sqlStatementCart = @"SELECT * FROM Cart";
            string slqStatementCartItems = @"Select * From CartItem JOIN Products ON CartItem.ProductId = Products.Id
                                            Where CartItem.CartId = @cartId";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                SqlCommand sqlCommandCart = new SqlCommand(sqlStatementCart, sqlConnection);



                //sqlCommand.Parameters.AddWithValue("@id", product.Id);
                
                try
                {
                    sqlConnection.Open();
                    SqlDataReader cartReader = sqlCommandCart.ExecuteReader();
                    while (cartReader.Read())
                    {

                        CartModel cart = new CartModel
                        {
                            CartId = (Int64)cartReader[0],
                            UserName = (string)cartReader[1],
                            PaymentType = (string)cartReader[2],
                            PaymentAmount = (decimal)cartReader[3],
                            CartItems = new List<CartItemModel>()
                        };

                        cartlList.Add(cart);
                    }
                    cartReader.Close();
                    foreach (var item in cartlList)
                    {
                        SqlCommand sqlCommandCartItems = new SqlCommand(slqStatementCartItems, sqlConnection);
                        sqlCommandCartItems.Parameters.AddWithValue("@cartId", item.CartId);
                        SqlDataReader cartItemReader = sqlCommandCartItems.ExecuteReader();
                        List<CartItemModel> cartItems = new List<CartItemModel>();
                        while (cartItemReader.Read())
                        {
                            cartItems.Add(new CartItemModel
                            {
                                CartItemId = (Int64)cartItemReader[0],
                                CartId = (Int64)cartItemReader[1],
                                ProductId = (int)cartItemReader[2],
                                Price = (decimal)cartItemReader[3],
                                Quantity = (int)cartItemReader[4],
                                ProductDetail = new ProductModel
                                {
                                    Id = (int)cartItemReader[5],
                                    Name = (string)cartItemReader[6],
                                    Price = (decimal)cartItemReader[7],
                                    Description = (string)cartItemReader[8]
                                }
                            });
                        }
                        cartItemReader.Close();
                        item.CartItems = cartItems;
                    }




                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

                return (cartlList);
            }
        }
        public List<CartModel> GetCartByUser(string username)
        {
            List<CartModel> cartlList = new List<CartModel>();
           
           
            string sqlStatementCart = @"SELECT CartId, UserName, PaymentType, PaymentAmount FROM Cart WHERE UserName = @username";
            string slqStatementCartItems = @"Select * From CartItem JOIN Products ON CartItem.ProductId = Products.Id
                                            Where CartItem.CartId = @cartId";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                SqlCommand sqlCommandCart = new SqlCommand(sqlStatementCart, sqlConnection);
                


                //sqlCommand.Parameters.AddWithValue("@id", product.Id);
                sqlCommandCart.Parameters.AddWithValue("@username", username);

                try
                {
                    sqlConnection.Open();
                    SqlDataReader cartReader = sqlCommandCart.ExecuteReader();
                    while (cartReader.Read())
                    {

                        CartModel cart = new CartModel
                        {
                            CartId = (Int64)cartReader[0],
                            UserName = (string)cartReader[1],
                            PaymentType = (string)cartReader[2],
                            PaymentAmount = (decimal)cartReader[3],
                            CartItems = new List<CartItemModel>()
                        };

                        cartlList.Add(cart);
                    }
                    cartReader.Close();
                    foreach (var item in cartlList)
                    {
                        SqlCommand sqlCommandCartItems = new SqlCommand(slqStatementCartItems, sqlConnection);
                        sqlCommandCartItems.Parameters.AddWithValue("@cartId", item.CartId);
                        SqlDataReader cartItemReader = sqlCommandCartItems.ExecuteReader();
                        List<CartItemModel> cartItems = new List<CartItemModel>();
                        while (cartItemReader.Read())
                        {
                            cartItems.Add(new CartItemModel
                            {
                                CartItemId = (Int64)cartItemReader[0],
                                CartId = (Int64)cartItemReader[1],
                                ProductId = (int)cartItemReader[2],
                                Price = (decimal)cartItemReader[3],
                                Quantity = (int)cartItemReader[4],
                                ProductDetail = new ProductModel
                                {
                                    Id = (int)cartItemReader[5],
                                    Name = (string)cartItemReader[6],
                                    Price = (decimal)cartItemReader[7],
                                    Description = (string)cartItemReader[8]
                                }
                            });
                        }
                        cartItemReader.Close();
                        item.CartItems = cartItems;
                    }
                        
                       
                    

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

                return (cartlList);
            }
        }
        public int InsertCart(CartModel cart)
        {

            int i = -1;
            //SELECT SCOPE_IDENTITY() METHOD returns Idenity of the last inserted row in the table
            string sqlStatementParent = @"INSERT INTO Cart VALUES(@username, @paymentType , @paymentAmount); 
                                            SELECT SCOPE_IDENTITY()";
            // executing stored procedure
            string sqlStatementChild = @"EXEC sp_InsertCartItem @CartId, @ProductId, @Price, @Quantity";
            


            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                SqlCommand sqlCommandParent = new SqlCommand(sqlStatementParent, sqlConnection);
                
                
                //sqlCommand.Parameters.AddWithValue("@id", product.Id);
                sqlCommandParent.Parameters.AddWithValue("@username", cart.UserName);
                sqlCommandParent.Parameters.AddWithValue("@paymentType", cart.PaymentType);
                sqlCommandParent.Parameters.AddWithValue("@paymentAmount", cart.PaymentAmount);
                try
                {
                    sqlConnection.Open();
                    i = (int)(decimal)sqlCommandParent.ExecuteScalar();

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
               
                try
                {
                   
                    foreach (var item in cart.CartItems)
                    {
                        //sqlCommandChild.Parameters.AddWithValue("@CartId", item.CartId);
                        SqlCommand sqlCommandChild = new SqlCommand(sqlStatementChild, sqlConnection);
                        sqlCommandChild.Parameters.AddWithValue("@CartId", i);
                        sqlCommandChild.Parameters.AddWithValue("@ProductId", item.ProductId);
                        sqlCommandChild.Parameters.AddWithValue("@Price", item.Price);
                        sqlCommandChild.Parameters.AddWithValue("@Quantity", item.Quantity);
                        sqlCommandChild.ExecuteNonQuery();
                        
                        

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    
                    
                }
            }
            return i;
        }
    }
}
