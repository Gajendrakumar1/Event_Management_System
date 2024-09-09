using Event_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

namespace Event_Management_System.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Student_tbl u)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                if (ModelState.IsValid == true)
                {
                    var MobNum = db.Student_tbl
                         .Where(model => model.Mobile == u.Mobile).FirstOrDefault();

                    if (MobNum == null)
                    {
                        Session["Mobile"] = u.Mobile;
                        //  Session["Type"] = u.type;

                        return RedirectToAction("Create", "User");
                    }
                    else
                    {
                        //string a = u.@type;
                        Session["userid"] = MobNum.Student_id;
                        //Session["Type"] = MobNum.type;
                        Session["Mobile"] = MobNum.Mobile;
                        Session["Name"] = MobNum.Name;
                        Session["College"] = MobNum.College_Tbl.College_Name;
                        Session["Collegeid"] = MobNum.College_Tbl.College_id;
                        return RedirectToAction("Index", "BookingInfo");
                    }
                }
                return View();
            }
        }
    }
}