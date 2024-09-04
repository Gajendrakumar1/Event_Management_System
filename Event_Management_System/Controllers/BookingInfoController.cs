using Event_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;

namespace Event_Management_System.Controllers
{
    public class BookingInfoController : Controller
    {
        // GET: BookingInfo
        public ActionResult Index()
        {
            
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                List<BookingInfoVM> bookingInfo = new List<BookingInfoVM>();
                var BookInfo = db.BookingInfo_tbl.Include(i => i.Event_Tbl).Include(i => i.Event_Tbl.EventMaster_Tbl).ToList();
                // ViewBag.BookingInfo = BookInfo;
                foreach (var i in BookInfo)
                {
                    bookingInfo.Add(new BookingInfoVM
                    {
                        Event_mst_Id = int.Parse(i.fk_eventid.ToString()),
                        Event_name = i.Event_Tbl.EventMaster_Tbl.Event_Name.ToString(),
                        booking_date = i.booking_date,
                        booking_time = i.booking_time,
                        TotalbookSeat = i.TotalbookSeat,
                        TotalPrice = i.TotalPrice
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
                var evtData = db.EventMaster_Tbl.ToList();
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

        public ActionResult AddBookInfo(BookingInfoVM vm)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                BookingInfo_tbl arg = new BookingInfo_tbl();
                arg.Booking_id = vm.Booking_id;
                arg.booking_date = vm.booking_date;
                arg.booking_time = vm.booking_time;
                arg.TotalbookSeat = vm.TotalbookSeat;
                arg.TotalPrice = vm.TotalPrice;
                arg.Created_Date = DateTime.Now;
                arg.Created_by = 1;
                arg.fk_student_id = 4;
                arg.fk_eventid= vm.Event_Id;

                db.BookingInfo_tbl.Add(arg);
                db.SaveChanges();
                return RedirectToAction("Index");
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
                data.booking_date = arg.booking_date;
                data.booking_time = arg.booking_time;
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