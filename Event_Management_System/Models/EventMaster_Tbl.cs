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
    
    public partial class EventMaster_Tbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EventMaster_Tbl()
        {
            this.Event_Tbl = new HashSet<Event_Tbl>();
            this.Seat_tbl = new HashSet<Seat_tbl>();
        }
    
        public int Event_Id { get; set; }
        public string Event_Name { get; set; }
        public Nullable<int> FKCollege_ID { get; set; }
        public System.DateTime Created_Date { get; set; }
        public Nullable<int> Created_by { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event_Tbl> Event_Tbl { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Seat_tbl> Seat_tbl { get; set; }
        public virtual College_Tbl College_Tbl { get; set; }
    }
}
