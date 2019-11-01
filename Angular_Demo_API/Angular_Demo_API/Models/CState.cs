using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angular_Demo_API.Models
{
    public class CState
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        public int? CountryId { get; set; }
    }
}