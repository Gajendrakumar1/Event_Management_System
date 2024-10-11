using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event_Management_System.Models
{
    public class ColgUser
    {
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string mobileNo { get; set; }
        public string EmailId { get; set; }
        public Nullable<int> CollegeID { get; set; }
        public string EmpCode { get; set; }
        public Nullable<System.DateTime> AccessFromdate { get; set; }
        public Nullable<System.DateTime> AccessTodate { get; set; }
        public string Period { get; set; }
        public System.DateTime createdon { get; set; }
        public int createdby { get; set; }
        public int College_id { get; set; }
        public string College_Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string InitialName { get; set; }
        public System.DateTime Created_Date { get; set; }
        public Nullable<int> Created_by { get; set; }

    }
}