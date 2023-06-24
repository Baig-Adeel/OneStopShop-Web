using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppStorePractice.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        public string toString() 
        {
            return "Name: " + UserName + " Password: " + Password;
        
        }
    }
    
}
