using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event_Management_System.Models
{
    public class SeatEvtLocVM
    {
        public int Seat_id { get; set; }
        public string Seat_Name { get; set; }
        public string Seat_row { get; set; }
        public int Event_Id { get; set; }
        public string Event_Name { get; set; }
        public int EventLoc_ID { get; set; }
        public string Location { get; set; }
       
    }
}