using Event_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace Event_Management_System.Controllers
{
    public class YearController : BaseController
    {
        public YearController(MenuService menuService) : base(menuService)
        {
        }
       
        // GET: Year
        public ActionResult Index()
        {
            using(Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                List<Year_Tbl> year = db.Year_Tbl.ToList();
                return View(year);
            }
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult AddYear(Year_Tbl y)
        {
            using(Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                y.Created_Date = DateTime.Now;
                y.Created_by = 1;
                db.Year_Tbl.Add(y);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(int Id)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.Year_Tbl.Where(x => x.Year_id == Id).FirstOrDefault();
                return View(data);
            }
        }

        public ActionResult UpdateYear(Years args)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.Year_Tbl.Where(x => x.Year_id == args.Year_id).FirstOrDefault();
                data.Year = args.Year;
                data.Created_Date = args.Created_Date;
                data.Created_by = args.Created_by;  

                db.Year_Tbl.Attach(data);
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Remove(int Id)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.Year_Tbl.Where(x => x.Year_id==Id).FirstOrDefault();
                db.Year_Tbl.Remove(data);
                db.Entry(data).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}