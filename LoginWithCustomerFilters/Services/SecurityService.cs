//using LoginWithCustomFilters.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppStorePractice.Models;

namespace WebAppStorePractice.Services
{
    public class SecurityService : ISecurityService
    {
        // List<UserModel> KnownUsers = new List<UserModel>();
        IUsersDAO _usersDAO { get; set; }
        public SecurityService(IUsersDAO users)
        {
            _usersDAO = users;
         //   KnownUsers.Add(new UserModel { Id = 0, UserName = "Adeel", Password = "abcd" });
          //  KnownUsers.Add(new UserModel { Id = 1, UserName = "Baig", Password = "abcde" });
           
        }
        public bool IsValid(UserModel user)
        {

            // Return True if found in the list
            //return KnownUsers.Any(x => x.UserName == user.UserName && x.Password == user.Password);
            return _usersDAO.FindUserByNameAndPassword(user);
        }
        public bool IsUnique(string userName)
        {
            if (_usersDAO.FindUserByUserName(userName))
            {
                return false;
            }
            else 
            {
                return true;
            }
                    
        }
       
    }
}
