using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event_Management_System.Models
{
    public class EvtLocationCollegeVM
    {
        public int EventLoc_ID { get; set; }
        public string Location { get; set; }
        public int No_of_Seat { get; set; }
        public int No_Of_Row { get; set; }
        
        public System.DateTime Created_Date { get; set; }
        public int? Created_by { get; set; }

        public int College_id { get; set; }
        public string College_Name { get; set; }
    }
}