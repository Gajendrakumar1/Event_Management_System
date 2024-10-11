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
    public class EventController : BaseController
    {
        public EventController(MenuService menuService) : base(menuService)
        {
        }
        // GET: Event
        public ActionResult Index()
        {
            if (Session["Collegeid"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int colgid = int.Parse(Session["Collegeid"].ToString());
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                List<Event_Tbl> evtLoc = db.Event_Tbl.Include(i => i.EventMaster_Tbl).Include(i => i.EventLocationMaster_Tbl).Where(a=>a.EventLocationMaster_Tbl.FKCollege_ID== colgid).ToList();
                return View(evtLoc);
            }
        }
        public ActionResult Add()
        {
            if (Session["Collegeid"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int colgid = int.Parse(Session["Collegeid"].ToString());
            List<EventMaster_Tbl> evtList = new List<EventMaster_Tbl>();
            List<EventLocationMaster_Tbl> locList = new List<EventLocationMaster_Tbl>();

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

                var locData = db.EventLocationMaster_Tbl.Where(a => a.FKCollege_ID == colgid).ToList();
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

        public ActionResult AddEvent(EventVM vm)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                Event_Tbl arg = new Event_Tbl();

                arg.Event_FromDate = vm.Event_FromDate;
                arg.Event_ToDate = vm.Event_ToDate;
                arg.Event_FromTime = vm.Event_FromTime;
                arg.Event_ToTime = vm.Event_ToTime;
                arg.PriceperSeat = vm.PriceperSeat;
                arg.maxSeatbookperuser = vm.maxSeatbookperuser;
                arg.Price = vm.Price;

                arg.Created_Date = DateTime.Now;
                arg.Created_by = 1;
                arg.FK_Event_Mst_Id = vm.Event_Id;
                arg.fk_Event_Loc_id = vm.EventLoc_ID;
                
                db.Event_Tbl.Add(arg);
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
                var data = db.Event_Tbl.Where(x => x.Event_Id == id).FirstOrDefault();

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

                var evt = new EventVM
                {
                    EventMast_Id = data.FK_Event_Mst_Id.Value,
                    Event_Id = data.Event_Id,
                    EventLoc_ID = data.fk_Event_Loc_id.Value,
                    Event_FromDate = data.Event_FromDate,
                    Event_ToDate = data.Event_ToDate,
                    Event_FromTime = data.Event_FromTime,
                    Event_ToTime = data.Event_ToTime,
                    PriceperSeat = data.PriceperSeat,
                    maxSeatbookperuser = data.maxSeatbookperuser,
                    Price = data.Price,
                };

                return View(evt);
            }
        }

        public ActionResult UpdateEvent(EventVM vm)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.Event_Tbl.Where(x => x.Event_Id == vm.Event_Id).FirstOrDefault();
                
                
                
                data.FK_Event_Mst_Id = vm.EventMast_Id;
                data.Event_Id = vm.Event_Id;
                data.fk_Event_Loc_id= vm.EventLoc_ID;
                data.Event_FromDate = vm.Event_FromDate;
                data.Event_ToDate = vm.Event_ToDate;
                data.Event_FromTime = vm.Event_FromTime;
                data.Event_ToTime = vm.Event_ToTime;
                data.PriceperSeat = vm.PriceperSeat;
                data.maxSeatbookperuser = vm.maxSeatbookperuser;
                data.Price = vm.Price;

                data.Created_Date = DateTime.Now;
                data.Created_by = 1;

                db.Event_Tbl.Attach(data);
                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Remove(int id)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.Event_Tbl.Where(x => x.Event_Id == id).FirstOrDefault();
                db.Event_Tbl.Remove(data);
                db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}