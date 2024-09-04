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
    public class EventLocationMasterController : Controller
    {
        // GET: EventLocationMaster
        public ActionResult Index()
        {
            using(Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                List<EventLocationMaster_Tbl> evtLoc = db.EventLocationMaster_Tbl.Include(i => i.College_Tbl).ToList();
                return View(evtLoc);
            }
            
        }

        public ActionResult Add()
        {

            List<College_Tbl> collegeList = new List<College_Tbl>();
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
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

                
                return View();
            }
        }

        public ActionResult AddEvtLocation(EvtLocationCollegeVM vm)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                EventLocationMaster_Tbl arg = new EventLocationMaster_Tbl();

                arg.Location = vm.Location;
                arg.No_of_Seat = vm.No_of_Seat;
                arg.No_Of_Row = vm.No_Of_Row;


                arg.Created_Date = DateTime.Now;
                arg.Created_by = 1;
                arg.FKCollege_ID = vm.College_id;

                db.EventLocationMaster_Tbl.Add(arg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                
        }

        public ActionResult Edit(int id)
        {
            List<College_Tbl> collegeList = new List<College_Tbl>();

            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.EventLocationMaster_Tbl.Where(x => x.EventLoc_ID == id).FirstOrDefault();

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

                var LocationVM = new EvtLocationCollegeVM
                {
                    College_Name = data.College_Tbl.College_Name,
                    College_id = data.College_Tbl.College_id,
                    EventLoc_ID = data.EventLoc_ID,
                    Location = data.Location,   
                    No_of_Seat = data.No_of_Seat,
                    No_Of_Row = data.No_Of_Row,
                };
                return View(LocationVM);
            }
        }

        public ActionResult UpdateEvtLocation(EvtLocationCollegeVM vm)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.EventLocationMaster_Tbl.Where(x => x.EventLoc_ID == vm.EventLoc_ID).FirstOrDefault();

                data.FKCollege_ID = vm.College_id;
                data.EventLoc_ID = vm.EventLoc_ID;
                data.Location = vm.Location;
                data.No_of_Seat = vm.No_of_Seat;
                data.No_Of_Row = vm.No_Of_Row;
                data.Created_Date = DateTime.Now;
                data.Created_by = 1;

                db.EventLocationMaster_Tbl.Attach(data);
                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Remove(int id)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.EventLocationMaster_Tbl.Where(x => x.EventLoc_ID == id).FirstOrDefault();
                db.EventLocationMaster_Tbl.Remove(data);
                db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}