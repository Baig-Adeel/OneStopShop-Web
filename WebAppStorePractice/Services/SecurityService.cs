using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppStorePractice.Models;

namespace WebAppStorePractice.Services
{
    public class SecurityService
    {
        // List<UserModel> KnownUsers = new List<UserModel>();
        UsersDAO usersDAO = new UsersDAO();
        public SecurityService()
        {
         //   KnownUsers.Add(new UserModel { Id = 0, UserName = "Adeel", Password = "abcd" });
          //  KnownUsers.Add(new UserModel { Id = 1, UserName = "Baig", Password = "abcde" });
           
        }
        public bool IsValid(UserModel user)
        {

            // Return True if found in the list
            //return KnownUsers.Any(x => x.UserName == user.UserName && x.Password == user.Password);
            return usersDAO.FindUserByNameAndPassword(user);
        }

    }
}
