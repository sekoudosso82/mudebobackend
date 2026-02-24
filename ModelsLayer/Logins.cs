using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLayer
{
    public class Logins
    {
        [Key, Required]
        public int LoginId { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        public Logins() { }
        public Logins(int loginId, string username, string password) { 
            LoginId = loginId;
            UserName = username;
            Password = password;
        }
    }
}
