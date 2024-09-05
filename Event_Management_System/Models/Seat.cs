using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event_Management_System.Models
{
    public class Seat
    {
        public string Row { get; set; }
        public int Number { get; set; }
        public string Label => $"{Row}{Number}";
    }
}