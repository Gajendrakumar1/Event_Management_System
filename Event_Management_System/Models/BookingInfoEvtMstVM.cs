using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event_Management_System.Models
{
    public class BookingInfoEvtMstVM
    {
        public int Booking_id { get; set; }
        public System.DateTime booking_date { get; set; }
        public string booking_time { get; set; }
        public int TotalbookSeat { get; set; }
        public decimal TotalPrice { get; set; }
        public System.DateTime Created_Date { get; set; }
        public Nullable<int> Created_by { get; set; }
        public int Event_Id { get; set; }
        public string Event_Name { get; set; }
    }
}