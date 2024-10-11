using Event_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using System.Web.UI;

namespace Event_Management_System.Controllers
{
    public class EventMasterController : BaseController
    {
        public EventMasterController(MenuService menuService) : base(menuService)
        {
        }
        // GET: EventMaster
        public ActionResult Index()
        {
            using(Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                if (Session["Collegeid"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                int colgid = int.Parse(Session["Collegeid"].ToString());
                var evt = db.EventMaster_Tbl.Include(x => x.College_Tbl).Where(a=>a.College_Tbl.College_id== colgid).ToList();
                return View(evt);
            }
        }

        public ActionResult Add()
        {
            if (Session["Collegeid"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int colgid = int.Parse(Session["Collegeid"].ToString());
            List<College_Tbl> collegeList = new List<College_Tbl>();
            using(Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var collegeData = db.College_Tbl.Where(c=>c.College_id== colgid).ToList();
                foreach (var item in collegeData)
                {
                    collegeList.Add(new College_Tbl
                    {
                        College_id = int.Parse(item.College_id.ToString()),
                        College_Name = item.College_Name.ToString(),
                    });
                }

                ViewBag.CollegeData = new SelectList(collegeList, "college_id", "college_name");

                return View();
            }
        }

        [HttpPost]
        public ActionResult AddEventMast(EventCollegeVM vm)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                EventMaster_Tbl args = new EventMaster_Tbl();
                
                args.Created_Date = DateTime.Now;
                args.Created_by = 1;
                args.FKCollege_ID = vm.College_id;
                db.EventMaster_Tbl.Add(args);
                args.Event_Name = vm.Event_Name;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(int id)
        {
            List<College_Tbl> collegeList = new List<College_Tbl>();

            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var eventEntity = db.EventMaster_Tbl.Where(x => x.Event_Id == id).FirstOrDefault();

                var collegeData = db.College_Tbl.ToList();
                foreach (var item in collegeData)
                {
                    collegeList.Add(new College_Tbl
                    {
                        College_id = int.Parse(item.College_id.ToString()),
                        College_Name = item.College_Name.ToString(),
                    });
                }

                ViewBag.CollegeData = new SelectList(collegeList, "college_id", "college_name");

                var eventVM = new EventCollegeVM
                {
                    Event_Id = eventEntity.Event_Id,
                    Event_Name = eventEntity.Event_Name,
                    College_id = eventEntity.FKCollege_ID ?? 0, 
                    College_Name = eventEntity.College_Tbl.College_Name 
                };

                return View(eventVM);
            }
        }

        public ActionResult UpdateEvents(EventCollegeVM vm)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.EventMaster_Tbl.Where(x => x.Event_Id == vm.Event_Id).FirstOrDefault();
                data.Event_Name = vm.Event_Name;
                data.Event_Id = vm.Event_Id;
                data.Event_Name = vm.Event_Name;
                data.FKCollege_ID = vm.College_id;
                
                data.Created_Date = DateTime.Now;
                data.Created_by = 1;

                db.EventMaster_Tbl.Attach(data);
                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Remove(int id)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.EventMaster_Tbl.Where(x => x.Event_Id == id).FirstOrDefault();
                db.EventMaster_Tbl.Remove(data);
                db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}