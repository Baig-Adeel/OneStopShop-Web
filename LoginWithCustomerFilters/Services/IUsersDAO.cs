using System.Collections.Generic;
using WebAppStorePractice.Models;

namespace WebAppStorePractice.Services
{
    public interface IUsersDAO
    {
        public List<UserModel> GetAllUsers();
        public bool FindUserByNameAndPassword(UserModel users);
        public bool FindUserByUserName(string userName);
        public bool RegisterNewUser(UserModel users);
    }
}
