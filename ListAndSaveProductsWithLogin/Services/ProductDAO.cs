using ListAndSaveProducts.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ListAndSaveProducts.Services
{
    public class ProductDAO : IProductDataService
    {
        public List<ProductModel> productList = new List<ProductModel>();
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public int Delete(int Id)
        {    
            // public int Delete(int id)
        
            int i = -1;
            string sqlStatement = "Delete FROM dbo.Products WHERE Id =  @id";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@id", Id);
                
                try
                {
                    sqlConnection.Open();
                    i = sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            return i;
        }

        public List<ProductModel> GetAllProducts()
        {
            string sqlStatement = "Select * from dbo.Products";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString)) 
            {
                            
                SqlCommand sqlCommand = new SqlCommand(sqlStatement,sqlConnection);
                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        productList.Add(new ProductModel { Id = (int)reader[0], Name = (string)reader[1], Price = (decimal)reader[2], Description = (string)reader[3] });
                        
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            return productList;
            
        }

        public ProductModel GetProductById(int Id)
        {
            ProductModel product = null;
            string sqlStatement = "Select * from dbo.Products Where Id = @id";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@id", Id);
                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        product = new ProductModel { Id = (int)reader[0], Name = (string)reader[1], Price = (decimal)reader[2], Description = (string)reader[3] };

                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            return product;
        }

        public int Insert(ProductModel product)
        {

            int i = -1;
            string sqlStatement = "INSERT INTO dbo.Products VALUES(@name , @price , @desc)" ;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection);

                //sqlCommand.Parameters.AddWithValue("@id", product.Id);
                sqlCommand.Parameters.AddWithValue("@name", product.Name);
                sqlCommand.Parameters.AddWithValue("@price", product.Price);
                sqlCommand.Parameters.AddWithValue("@desc", product.Description);
                try
                {
                    sqlConnection.Open();
                    i = sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            return i;
        }

        public List<ProductModel> SearchProducts(string SearchTerm)
        {
            string sqlStatement = "Select * from dbo.Products Where Name LIKE @Name";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Name", '%' + SearchTerm + '%');
                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        productList.Add(new ProductModel { Id = (int)reader[0], Name = (string)reader[1], Price = (decimal)reader[2], Description = (string)reader[3] });

                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            return productList;

        }
    

        public int Update(ProductModel product)
        {
            int i = -1;
            string sqlStatement = "Update dbo.Products SET Name = @name , Price = @price , Description = @desc WHERE Id =  @id";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@id", product.Id);
                sqlCommand.Parameters.AddWithValue("@name", product.Name);
                sqlCommand.Parameters.AddWithValue("@price", product.Price);
                sqlCommand.Parameters.AddWithValue("@desc", product.Description);
                try
                {
                    sqlConnection.Open();
                    i = sqlCommand.ExecuteNonQuery();
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
