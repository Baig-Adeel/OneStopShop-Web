using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAppStorePractice.Models;

namespace WebAppStorePractice.Services
{
    public class UsersDAO
    {
        String ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool FindUserByNameAndPassword(UserModel users)
        {
            bool success = false;
            string sqlStatement = "Select * from dbo.Users where UserName = @username and Password = @password";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = users.UserName;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 40).Value = users.Password;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        success = true;
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
                return success;
            }
        }
    }
}
