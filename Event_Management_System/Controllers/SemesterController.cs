using Event_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Event_Management_System.Controllers
{
    public class SemesterController : BaseController
    {
        public SemesterController(MenuService menuService) : base(menuService)
        {
        }
        // GET: Semester
        public ActionResult Index()
        {
            using(Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                List<Semester_Tbl> sem = db.Semester_Tbl.ToList();
                return View(sem);
            }
        }

        public ActionResult Add()
        {
            return View();
        }
        public ActionResult AddSemester(Semester_Tbl args)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                args.Created_Date = DateTime.Now;
                args.Created_by = 1;
                db.Semester_Tbl.Add(args);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(int id)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.Semester_Tbl.Where(x => x.Sem_id == id).FirstOrDefault();
                return View(data);
            }
                
        }
        public ActionResult UpdateSemester(Semester_Tbl args)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.Semester_Tbl.Where(x => x.Sem_id == args.Sem_id).FirstOrDefault();
                data.Sem_Name = args.Sem_Name;
                data.Created_Date = args.Created_Date;
                data.Created_by = args.Created_by;

                db.Semester_Tbl.Attach(data);
                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }

        }
        public ActionResult Remove(int id)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.Semester_Tbl.Where(x => x.Sem_id == id).FirstOrDefault();
                db.Semester_Tbl.Remove(data);
                db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }
    }
}