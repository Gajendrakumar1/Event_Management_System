using Event_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Event_Management_System.Controllers
{
    public class Booking_SeatInfoController : Controller
    {
        // GET: Booking_SeatInfo
        public ActionResult Index()
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                List<Booking_SeatInfo_tbl> seatInfo = db.Booking_SeatInfo_tbl.ToList();
                return View(seatInfo);
            }
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult AddBookSeat(Booking_SeatInfo_tbl arg)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                arg.Created_Date = DateTime.Now;
                arg.Created_by = 1;
                arg.fk_booking_id = 1;
                db.Booking_SeatInfo_tbl.Add(arg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }

        public ActionResult Edit(int id)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.Booking_SeatInfo_tbl.Where(x => x.bookingSeat_id == id).FirstOrDefault();
                return View(data);
            }
        }

        public ActionResult UpdateBookSeat(Booking_SeatInfo_tbl arg)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.Booking_SeatInfo_tbl.Where(x => x.bookingSeat_id == arg.bookingSeat_id).FirstOrDefault();
                

                data.Created_Date = arg.Created_Date;
                data.Created_by = arg.Created_by;

                db.Booking_SeatInfo_tbl.Attach(data);
                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Remove(int id)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.Booking_SeatInfo_tbl.Where(x => x.bookingSeat_id == id).FirstOrDefault();
                db.Booking_SeatInfo_tbl.Remove(data);
                db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}