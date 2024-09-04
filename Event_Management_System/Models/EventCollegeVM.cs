using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event_Management_System.Models
{
    public class EventCollegeVM
    {
        public int Event_Id { get; set; }
        public string Event_Name { get; set; }
        public int College_id { get; set; }
        public string College_Name { get; set; }
    }
}