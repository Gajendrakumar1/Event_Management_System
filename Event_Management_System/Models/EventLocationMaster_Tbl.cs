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
    
    public partial class EventLocationMaster_Tbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EventLocationMaster_Tbl()
        {
            this.BookingInfo_tbl = new HashSet<BookingInfo_tbl>();
            this.Event_Tbl = new HashSet<Event_Tbl>();
            this.Seat_tbl = new HashSet<Seat_tbl>();
        }
    
        public int EventLoc_ID { get; set; }
        public string Location { get; set; }
        public int No_of_Seat { get; set; }
        public int No_Of_Row { get; set; }
        public Nullable<int> FKCollege_ID { get; set; }
        public System.DateTime Created_Date { get; set; }
        public Nullable<int> Created_by { get; set; }
        public string Seattype { get; set; }
        public string rowvalue { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookingInfo_tbl> BookingInfo_tbl { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event_Tbl> Event_Tbl { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Seat_tbl> Seat_tbl { get; set; }
        public virtual College_Tbl College_Tbl { get; set; }
    }
}
