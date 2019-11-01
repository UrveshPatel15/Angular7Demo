using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angular_Demo_API.Models
{
    public class CEmployee
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddelName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public string Image { get; set; }
        public string ImageBase64 { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
        public DateTime? JoinDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Skill { get; set; }
        public string Mobile { get; set; }
        public string Description { get; set; }
        public int? Country { get; set; }
        public string CountryName { get; set; }
        public int? State { get; set; }
        public string StateName { get; set; }
        public int? City { get; set; }
        public string CityName { get; set; }
        public string ZipCode { get; set; }
        public string Relationship { get; set; }
    }
}