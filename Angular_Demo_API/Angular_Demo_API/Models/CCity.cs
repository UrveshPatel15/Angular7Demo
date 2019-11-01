using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angular_Demo_API.Models
{
    public class CCity
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public int? StateId { get; set; }
        public int? CountryId { get; set; }
    }
}