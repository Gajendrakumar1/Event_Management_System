using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event_Management_System.Models
{
    public class EventVM
    {
        public int Event_Id { get; set; }
        public Nullable<int> FK_Event_Mst_Id { get; set; }
        public System.DateTime Event_FromDate { get; set; }
        public System.DateTime Event_ToDate { get; set; }
        public string Event_FromTime { get; set; }
        public string Event_ToTime { get; set; }
        public Nullable<int> fk_Event_Loc_id { get; set; }
        public decimal PriceperSeat { get; set; }
        public int maxSeatbookperuser { get; set; }
        public decimal Price { get; set; }
        public System.DateTime Created_Date { get; set; }
        public Nullable<int> Created_by { get; set; }
        public int EventMast_Id { get; set; }
        public string Event_Name { get; set; }
        public int EventLoc_ID { get; set; }
        public string Location { get; set; }
    }
}