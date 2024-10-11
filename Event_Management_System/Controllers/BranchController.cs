using Event_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace Event_Management_System.Controllers
{
    public class BranchController : BaseController
    {
        public BranchController(MenuService menuService) : base(menuService)
        {
        }
        // GET: Branch
        public ActionResult Index()
        {
            using(Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                List<Branch_Tbl> branch = db.Branch_Tbl.ToList();
                return View(branch);
            }
        }

        public ActionResult Add()
        {
            return View();
        }
        public ActionResult AddBranch(Branch_Tbl args)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                args.Created_Date = DateTime.Now;
                args.Created_by = 1;
                db.Branch_Tbl.Add(args);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(int id)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.Branch_Tbl.Where(x => x.Branch_id == id).FirstOrDefault();
                return View(data);
            }
                
        }
        public ActionResult UpdateBranch(Branch_Tbl args)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.Branch_Tbl.Where(x => x.Branch_id == args.Branch_id).FirstOrDefault();
                data.Branch = args.Branch;
                data.Created_Date = DateTime.Now;
                data.Created_by = 1;

                db.Branch_Tbl.Attach(data);
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Remove(int id)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.Branch_Tbl.Where(x => x.Branch_id == id).FirstOrDefault();
                db.Branch_Tbl.Remove(data);
                db.Entry(data).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}