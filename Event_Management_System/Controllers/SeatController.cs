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
    public class SeatController : BaseController
    {
        public SeatController(MenuService menuService) : base(menuService)
        {
        }
        // GET: Seat
        public ActionResult Index()
        {
            if (Session["Collegeid"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int colgid = int.Parse(Session["Collegeid"].ToString());
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                List<Seat_tbl> seat = db.Seat_tbl.Include(i => i.EventMaster_Tbl).Include(i => i.EventLocationMaster_Tbl).Where(a=>a.EventLocationMaster_Tbl.FKCollege_ID== colgid).ToList();
                return View(seat);
            }

        }

        public ActionResult Add()
        {
            List<EventMaster_Tbl> evtList = new List<EventMaster_Tbl>();
            List<EventLocationMaster_Tbl> locList = new List<EventLocationMaster_Tbl>();
            if (Session["Collegeid"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int colgid = int.Parse(Session["Collegeid"].ToString());
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var evtData = db.EventMaster_Tbl.Where(a=>a.FKCollege_ID== colgid).ToList();
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

                var locData = db.EventLocationMaster_Tbl.Where(a=>a.FKCollege_ID== colgid).ToList();
                foreach (var i in locData)
                {
                    locList.Add(new EventLocationMaster_Tbl
                    {
                        EventLoc_ID = int.Parse(i.EventLoc_ID.ToString()),
                        Location = i.Location.ToString(),
                    });
                }
                ViewBag.LocData = new SelectList(locList, "EventLoc_ID", "Location");



                return View();
            }

        }

        public ActionResult AddSeat(SeatEvtLocVM vm)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                Seat_tbl arg = new Seat_tbl();

                arg.Seat_Name = vm.Seat_Name;
                arg.Seat_row = vm.Seat_row;

                arg.FK_EventLoc_ID = vm.EventLoc_ID;
                arg.fk_Even_Mst_id = vm.Event_Id;
                db.Seat_tbl.Add(arg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }

        public ActionResult Edit(int id)
        {
            List<EventMaster_Tbl> evtList = new List<EventMaster_Tbl>();
            List<EventLocationMaster_Tbl> locList = new List<EventLocationMaster_Tbl>();

            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.Seat_tbl.Where(x => x.Seat_id == id).FirstOrDefault();

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

                var locData = db.EventLocationMaster_Tbl.ToList();
                foreach (var i in locData)
                {
                    locList.Add(new EventLocationMaster_Tbl
                    {
                        EventLoc_ID = int.Parse(i.EventLoc_ID.ToString()),
                        Location = i.Location.ToString(),
                    });
                }
                ViewBag.LocData = new SelectList(locList, "EventLoc_ID", "Location");

                var seatLoc = new SeatEvtLocVM
                {
                    Seat_id = data.Seat_id,
                    Seat_Name = data.Seat_Name,
                    Seat_row = data.Seat_row,
                    EventLoc_ID = data.FK_EventLoc_ID.Value,
                    Event_Id = data.fk_Even_Mst_id.Value,
                };

                return View(seatLoc);
            }
        }

        public ActionResult UpdateSeat(SeatEvtLocVM vm)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.Seat_tbl.Where(x => x.Seat_id == vm.Seat_id).FirstOrDefault();
                data.Seat_id = vm.Seat_id;
                data.Seat_Name = vm.Seat_Name;
                data.Seat_row = vm.Seat_row;
                data.FK_EventLoc_ID = vm.EventLoc_ID;
                data.fk_Even_Mst_id = vm.Event_Id;
                db.Seat_tbl.Attach(data);
                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Remove(int id)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.Seat_tbl.Where(x => x.Seat_id == id).FirstOrDefault();
                db.Seat_tbl.Remove(data);
                db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}