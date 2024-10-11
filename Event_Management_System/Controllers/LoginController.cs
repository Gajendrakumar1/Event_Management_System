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
            var categories = new List<SelectListItem>
            {
                new SelectListItem { Text = "Student", Value = "Student" },
                new SelectListItem { Text = "Admin", Value = "Admin" }
            };

            ViewBag.catMsg = new SelectList(categories, "Value", "Text");
            return View();
        }

        [HttpPost]
        public ActionResult Index(Student_tbl u)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {

                if (ModelState.IsValid == true)
                {
                    if (u.Name == "Student")
                    {
                        var MobNum = db.Student_tbl
                             .Where(model => model.Mobile == u.Mobile).FirstOrDefault();

                        if (MobNum == null)
                        {
                            Session["Mobile"] = u.Mobile;
                            Session["Type"] = "Student";

                            return RedirectToAction("Create", "User");
                        }
                        else
                        {
                            //string a = u.@type;
                            Session["userid"] = MobNum.Student_id;
                            Session["Type"] = "Student";
                            Session["Mobile"] = MobNum.Mobile;
                            Session["Name"] = MobNum.Name;
                            Session["College"] = MobNum.College_Tbl.College_Name;
                            Session["Collegeid"] = MobNum.College_Tbl.College_id;
                            return RedirectToAction("Index", "BookingInfo");
                        }
                    }
                    else
                    {
                        var MobNum = db.User_tbl
                             .Where(model => model.mobileNo == u.Mobile).FirstOrDefault();

                        if (MobNum == null)
                        {
                            Session["Mobile"] = u.Mobile;
                            Session["Type"] = "Admin";

                            return RedirectToAction("Create", "User");
                        }
                        else
                        {
                            //string a = u.@type;
                            Session["userid"] = MobNum.AdminId;
                            Session["Type"] = "Admin";
                            Session["Mobile"] = MobNum.mobileNo;
                            Session["Name"] = MobNum.Name;
                            Session["College"] = MobNum.College_Tbl.College_Name;
                            Session["Collegeid"] = MobNum.College_Tbl.College_id;
                            return RedirectToAction("Index", "College");
                        }
                    }
                }
                return View();
            }
        }
    }
}