using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angular_Demo_API.Models
{
    public class CRegisterUser
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddelName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
        public string Image { get; set; }
        public string ImageBase64 { get; set; }
    }
}