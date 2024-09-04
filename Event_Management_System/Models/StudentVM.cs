using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event_Management_System.Models
{
    public class StudentVM
    {
        public int Student_id { get; set; }
        public string Student_Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        public int? college_id { get; set; }
        public string college_name { get; set; } 
        public int? year_id { get; set; }
        public int Year { get; set; } 
        public int? Sem_id { get; set; }
        public string Sem_Name { get; set; } 
        public DateTime Created_Date { get; set; }
        public int? Created_by { get; set; }
    }
}