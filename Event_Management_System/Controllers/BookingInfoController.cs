using Event_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Event_Management_System.Controllers
{
    public class BookingInfoController : Controller
    {
        // GET: BookingInfo
        public ActionResult Index()
        {
            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
               
                string mobnum = Session["Mobile"].ToString();
                List<BookingInfoVM> bookingInfo = new List<BookingInfoVM>();
                var BookInfo = db.BookingInfo_tbl.Include(i => i.Event_Tbl)
                    .Include(i => i.Event_Tbl.EventMaster_Tbl)
                     .Include(i => i.Student_tbl)
                    .Where(l => l.Student_tbl.Mobile== mobnum) .ToList();
                // ViewBag.BookingInfo = BookInfo;
                foreach (var i in BookInfo)
                {
                    bookingInfo.Add(new BookingInfoVM
                    {
                        Event_mst_Id = int.Parse(i.fk_eventid.ToString()),
                        Event_name = i.Event_Tbl.EventMaster_Tbl.Event_Name.ToString(),
                        booking_date = i.Event_Tbl.Event_FromDate,
                        booking_time = $"{i.Event_Tbl.Event_FromTime} - {i.Event_Tbl.Event_ToTime}",
                        TotalbookSeat = i.TotalbookSeat.Value,
                        TotalPrice = i.TotalPrice.Value
                    });
                }
                return View(bookingInfo);
            }
        }
        public ActionResult Add()
        {
            List<EventMaster_Tbl> evtList = new List<EventMaster_Tbl>();
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                int Vcolid = int.Parse(Session["Collegeid"].ToString());
                var evtData = db.EventMaster_Tbl.Where(i=>i.FKCollege_ID== Vcolid).ToList();
                foreach (var i in evtData)
                {
                    evtList.Add(new EventMaster_Tbl
                    {
                        Event_Id = int.Parse(i.Event_Id.ToString()),
                        Event_Name = i.Event_Name.ToString(),
                    }
                    );
                }

                ViewBag.EvtMstData = new SelectList(evtList, "Event_Id", "Event_Name");


                return View();
            }
        }
        public JsonResult GetSubcat(int categoryid)
        {
            List<EventLocationMaster_Tbl> EventLocation = new List<EventLocationMaster_Tbl>();
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
            
                var allData = db.Event_Tbl.Include(i=>i.EventLocationMaster_Tbl)
                          .Where(i => i.FK_Event_Mst_Id == categoryid)
                          .ToList();

                foreach (var item in allData)
                {
                    EventLocation.Add(new EventLocationMaster_Tbl
                    {
                        EventLoc_ID = item.EventLocationMaster_Tbl.EventLoc_ID,
                        Location = item.EventLocationMaster_Tbl.Location,
                    });

                }

                // Return the subcategories as JSON
                return Json(EventLocation, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetSeatDetails(int vEventid)
        {
            int userid =int.Parse(Session["userid"].ToString());
            var bookinginfo = new List<bookinginfo>();
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                //var BookInfo = db.BookingInfo_tbl.Include(i => i.Event_Tbl)
                //   .Include(i => i.Event_Tbl.EventMaster_Tbl)
                //    .Include(i => i.Student_tbl)
                //   .Where(l => l.Student_tbl.Student_id == userid && l.fk_eventid== vEventid).ToList();


                var data = db.Event_Tbl
                       .Include(i => i.EventLocationMaster_Tbl)
                       .Include(i => i.EventMaster_Tbl)
                .Where(i => i.EventLocationMaster_Tbl.EventLoc_ID == vEventid).ToList();

                // Explicitly load the Seats for each EventLocationMaster
                foreach (var e in data)
                {
                    db.Entry(e.EventLocationMaster_Tbl)
                      .Collection(l => l.Seat_tbl)
                      .Load();
                }


                foreach (var e in data)
                {
                    bookinginfo.Add(new bookinginfo
                    {
                        Event_FromDate = e.Event_FromDate.ToString(),
                        Event_ToDate = e.Event_ToDate.ToString(),
                        Event_FromTime = e.Event_FromTime.ToString(),
                        Event_ToTime = e.Event_ToTime.ToString(),
                        PriceperSeat = e.PriceperSeat.ToString(),
                        maxSeatbookperuser = e.maxSeatbookperuser.ToString(),
                        Location = e.EventLocationMaster_Tbl.Location.ToString(),
                        Event_Name = e.EventMaster_Tbl.Event_Name.ToString(),
                        No_of_Seat = e.EventLocationMaster_Tbl.No_of_Seat,
                        No_Of_Row = e.EventLocationMaster_Tbl.No_Of_Row,
                        Event_Id=e.Event_Id.ToString(),
                        Seattype = e.EventLocationMaster_Tbl.Seattype,
                        rowvalue = e.EventLocationMaster_Tbl.rowvalue,
                        SeatID= string.Join(", ", e.BookingInfo_tbl.Select(x => x.SeatID)),
                        //  Seat_Name = string.Join(", ", e.EventLocationMaster_Tbl.Seat_tbl.Select(s => s.Seat_id))+"-"+ string.Join(", ", e.EventLocationMaster_Tbl.Seat_tbl.Select(s => s.Seat_Name)) + "-" + string.Join(", ", e.EventLocationMaster_Tbl.Seat_tbl.Select(s => s.Seat_row)) // Assuming Seat_Name is a property of Seat_tbl
                        Seat_Name = string.Join(", ", e.EventLocationMaster_Tbl.Seat_tbl
            .Select(s => $"{s.Seat_id}-{s.Seat_Name}-{s.Seat_row}"))

                    });
                }
                // Return the subcategories as JSON
                return Json(bookinginfo, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AddBookInfo(string veventid, string vLocId, string vTotalbookSeat,string vTotalPrice,string vseatid)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                BookingInfo_tbl arg = new BookingInfo_tbl();
               // arg.booking_date = vm.booking_date;
                //arg.booking_time = vm.booking_time;
                arg.TotalbookSeat = int.Parse(vTotalbookSeat);
                arg.TotalPrice = decimal.Parse(vTotalPrice);
                arg.Created_Date = DateTime.Now;
                arg.Created_by = int.Parse(Session["userid"].ToString());
                arg.fk_student_id = int.Parse(Session["userid"].ToString());
                arg.fk_eventid= int.Parse(veventid);
                arg.fklocationid= int.Parse(vLocId);
                arg.SeatID = vseatid;
                db.BookingInfo_tbl.Add(arg);

                db.SaveChanges();

                string[] seatRows = vseatid.Split(',');

                for (int i = 0; i < seatRows.Length; i++)
                {
                   
                    var seat = new Booking_SeatInfo_tbl
                    {
                        fk_booking_id =arg.Booking_id, // Assign the first part to Seat_Name
                        FKSeatID = int.Parse(seatRows[i].ToString()), // Assign the first part to Seat_Name
                        Created_by=1,
                        Created_Date = DateTime.Now,
                    };

                    // Add the object to the DbSet
                    db.Booking_SeatInfo_tbl.Add(seat);

                }
                db.SaveChanges();
                TempData["Bookingid"] = arg.Booking_id;
                return Json(arg.Booking_id, JsonRequestBehavior.AllowGet);
                // return RedirectToAction("Index", "Ticket");
            }
        }

        public ActionResult Edit(int id)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.BookingInfo_tbl.Where(x => x.Booking_id == id).FirstOrDefault();
                return View(data);
            }
        }

        public ActionResult UpdateBookInfo(BookingInfo_tbl arg)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.BookingInfo_tbl.Where(x => x.Booking_id == arg.Booking_id).FirstOrDefault();
                //data.booking_date = arg.booking_date;
                //data.booking_time = arg.booking_time;
                data.TotalbookSeat = arg.TotalbookSeat;
                data.TotalPrice = arg.TotalPrice;


                data.Created_Date = arg.Created_Date;
                data.Created_by = arg.Created_by;

                db.BookingInfo_tbl.Attach(data);
                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Remove(int id)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.BookingInfo_tbl.Where(x => x.Booking_id == id).FirstOrDefault();
                db.BookingInfo_tbl.Remove(data);
                db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}