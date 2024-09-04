using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event_Management_System.Models
{
    public class Years
    {
        public int Year_id { get; set; }
        public string Year { get; set; }
        public DateTime Created_Date { get; set; }
        public int? Created_by { get; set; }
    }
}