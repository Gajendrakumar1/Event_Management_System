//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Event_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User_tbl
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
        public int isactive { get; set; }
        public int isdeleted { get; set; }
    
        public virtual College_Tbl College_Tbl { get; set; }
    }
}
