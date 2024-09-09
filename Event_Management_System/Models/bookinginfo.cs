using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event_Management_System.Models
{
    public class bookinginfo
    {
        public string Event_FromDate { get; set; }
        public string Event_ToDate { get; set; }
        public string Event_FromTime { get; set; }

        public string Event_ToTime { get; set; }
        public string PriceperSeat { get; set; }
        public string maxSeatbookperuser { get; set; }

        public string Location { get; set; }
        public string Event_Name { get; set; }
        public int Seat_id { get; set; }
        public string Seat_Name { get; set; }
        public string Seat_row { get; set; }
        public int No_of_Seat { get; set; }
        public int No_Of_Row { get; set; }
        public string Seattype { get; set; }
        public string rowvalue { get; set; }
        public string Event_Id { get; set; }
        public string SeatID { get; set; }
    }
}