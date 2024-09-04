using Event_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Event_Management_System.Controllers
{
    public class LoginPageController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {

            return View();
        }

        public ActionResult LoginAuth(Student_tbl args)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                //var mob_num = db.Student_tbl.Where(x => x.Mobile == args.Mobile).FirstOrDefault();
                //if (mob_num != null)
                //{
                //    Session["User"] = mob_num.Student_id;
                //    Session["Mobile"] = mob_num.Mobile;
                //    return RedirectToAction("Index", "Student");
                //}
                //else
                //{
                //    Session["Mobile"] = args.Mobile;

                //    return RedirectToAction("Add", "Student");
                //}
                return View();
            }
        }
    }
}