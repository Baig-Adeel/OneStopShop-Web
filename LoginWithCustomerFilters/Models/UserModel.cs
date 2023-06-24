using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppStorePractice.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [EmailAddress]
        [Display(Name = "Email Address")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string AccessLevel { get; set; }
        public string toString() 
        {
            return "Name: " + UserName + " Password: " + Password + " Access: " + AccessLevel;
        
        }

        
    }
    
}
