using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Event_Management_System.Models
{
    [Table("State_List")]
    public class States
    {
        public int State_Id { get; set; }
        public string State_Name { get; set; }
    }
}