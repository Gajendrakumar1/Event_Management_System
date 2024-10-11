using Event_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace Event_Management_System.Controllers
{
    public class CollegeController : BaseController
    {
        public CollegeController(MenuService menuService) : base(menuService)
        {
        }
        // GET: College
        public ActionResult Index()
        {
            using(Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                if (Session["Collegeid"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                int colgid = int.Parse(Session["Collegeid"].ToString());
                List<College_Tbl> CollegeData = db.College_Tbl.Where(a=>a.College_id== colgid).ToList();
                return View(CollegeData);
            }
        }

        // opens the Add page
        public ActionResult Add()
        {
            BindStates();
            return View();
        }

        private void BindStates()
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                List<State_List> UKStates = db.State_List.ToList();
                ViewBag.DDLStates = new SelectList(UKStates , "State_Name", "State_Name");
            }
        }

        // it will call on click event
        public ActionResult AddCollege(College_Tbl c)
        {
            BindStates();
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                c.Created_Date = DateTime.Now;
                c.Created_by = 1;
                db.College_Tbl.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
        }
        // opens the Edit page
        public ActionResult Edit(int CollegeId)
        {
            BindStates();
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.College_Tbl.Where(x => x.College_id == CollegeId).FirstOrDefault(); 
                return View(data); 
            }
        }
        // it will call on click event
        public ActionResult UpdateCollege(College_Tbl args)
        {
            BindStates();
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.College_Tbl.Where(x=>x.College_id == args.College_id).FirstOrDefault();
                data.College_Name = args.College_Name;
                data.Address = args.Address;
                data.City = args.City;
                data.State = args.State;
                data.PinCode = args.PinCode;
                data.Created_Date = DateTime.Now;
                data.Created_by = 1;

                db.College_Tbl.Attach(data);
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Remove(int collegeId)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.College_Tbl.Where(x => x.College_id == collegeId).FirstOrDefault();
                db.College_Tbl.Remove(data);
                db.Entry(data).State = EntityState.Deleted;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}