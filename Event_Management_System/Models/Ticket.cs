using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event_Management_System.Models
{
    public class Ticket
    {
        public int BookingId { get; set; }
        public string Seats { get; set; }
        public string EventName { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public string Venue { get; set; }
        public string QRCodeBase64 { get; set; }
        public string TicketPrice { get; set; }
        public string StudentName { get;set; }
        public string CollName { get; set; }

    }
}