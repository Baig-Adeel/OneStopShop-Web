//using LoginWithCustomFilters.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAppStorePractice.Models;

namespace WebAppStorePractice.Services
{
    public class UsersDAO : IUsersDAO
    {
        String ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

       
        public List<UserModel> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();
            string sqlStatement = "Select * from dbo.Users";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        users.Add(new UserModel { Id = (int)reader[0], UserName = (string)reader[1], Password = (string)reader[2] });
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
                return users;
            }
        }

        public bool FindUserByNameAndPassword(UserModel users)
        {
            bool success = false;
            string sqlStatement = "Select * from dbo.Users where UserName = @username and Password = @password";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar, 50).Value = users.UserName;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar, 50).Value = users.Password;
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
        public bool FindUserByUserName(string userName)
        {
            bool success = false;
            string sqlStatement = "Select * from dbo.Users where UserName = @username";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar, 50).Value = userName;
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
        public bool RegisterNewUser(UserModel users)
        {
            int i = -1;
            bool success = false;
            string sqlStatement = "Insert INTO dbo.Users VALUES(@username, @password, @accessLevel)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar, 40).Value = users.UserName;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar, 40).Value = users.Password;
                command.Parameters.Add("@accessLevel", System.Data.SqlDbType.Char, 30).Value = users.AccessLevel;
                try
                {
                    connection.Open();
                    i = command.ExecuteNonQuery();
                    if (i != -1)
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
