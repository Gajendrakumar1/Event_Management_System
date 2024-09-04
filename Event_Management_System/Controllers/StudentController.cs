using Event_Management_System.Models;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Event_Management_System.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student


        public ActionResult Index()
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                //long mob = long.Parse(Session["Mobile"].ToString());

                //List<Student_tbl> stu = db.Student_tbl.Where(x => x.Mobile == mob.ToString()).ToList();

                var stu = db.Student_tbl.Include(i => i.College_Tbl).Include(i => i.Year_Tbl).Include(i => i.Semester_Tbl).ToList();
                return View(stu);
            }
        }

        public ActionResult Add()
        {
            BindStates();

            List<College_Tbl> collegeList = new List<College_Tbl>();
            List<Year_Tbl> yearList = new List<Year_Tbl>();
            List<Semester_Tbl> semesterList = new List<Semester_Tbl>();

            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                //College Drop Down
                var collData = db.College_Tbl.ToList();
                foreach (var i in collData)
                {
                    collegeList.Add(new College_Tbl
                    {
                        College_id = int.Parse(i.College_id.ToString()),
                        College_Name = i.College_Name.ToString(),
                    });
                }
                ViewBag.CollegeData = new SelectList(collegeList, "college_id", "college_name");
                

                //Year Drop Down
                var yearData = db.Year_Tbl.ToList();
                foreach (var i in yearData)
                {
                    yearList.Add(new Year_Tbl
                    {
                        Year_id = int.Parse(i.Year_id.ToString()),
                        Year = i.Year.ToString(),
                    });
                }


                ViewBag.YearData = new SelectList(yearList, "year_id", "Year");


                //Semester Drop Down
                var semData = db.Semester_Tbl.ToList();
                foreach(var i in semData)
                {
                    semesterList.Add(new Semester_Tbl
                    {
                        Sem_id = int.Parse(i.Sem_id.ToString()),
                        Sem_Name = i.Sem_Name.ToString(),
                    });
                }

                ViewBag.SemData = new SelectList(semesterList, "Sem_id", "Sem_Name");

                //string mob_num = Session["Mobile"] as string;
                //ViewBag.MobileNum = mob_num;

                return View();

            }
        }
        

        public ActionResult AddStudent(StudentVM vm)
        {
            BindStates();
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                Student_tbl stu = new Student_tbl();

                stu.Student_id = vm.Student_id;
                stu.Student_Code = vm.Student_Code;
                stu.Name = vm.Name;
                stu.Address = vm.Address;
                stu.City = vm.City;
                stu.State = vm.State;
                stu.PinCode = vm.PinCode;
                stu.Mobile = vm.Mobile;
                stu.EmailId = vm.EmailId;

                stu.FKCollege_ID = vm.college_id;
                stu.FkYearId = vm.year_id;
                stu.FKSemID = vm.Sem_id;

                stu.Created_Date = DateTime.Now;
                stu.Created_by = 1;


                db.Student_tbl.Add(stu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            BindStates();

            List<College_Tbl> collegeList = new List<College_Tbl>();
            List<Year_Tbl> yearList = new List<Year_Tbl>();
            List<Semester_Tbl> semesterList = new List<Semester_Tbl>();

            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var studentData = db.Student_tbl.Where(x => x.Student_id == id).FirstOrDefault();
                var collData = db.College_Tbl.ToList();
                foreach (var i in collData)
                {
                    collegeList.Add(new College_Tbl
                    {
                        College_id = int.Parse(i.College_id.ToString()),
                        College_Name = i.College_Name.ToString(),
                    });
                }
                ViewBag.CollegeData = new SelectList(collegeList, "college_id", "college_name");


                //Year Drop Down
                var yearData = db.Year_Tbl.ToList();
                foreach (var i in yearData)
                {
                    yearList.Add(new Year_Tbl
                    {
                        Year_id = int.Parse(i.Year_id.ToString()),
                        Year = i.Year.ToString(),
                    });
                }


                ViewBag.YearData = new SelectList(yearList, "year_id", "Year");


                //Semester Drop Down
                var semData = db.Semester_Tbl.ToList();
                foreach (var i in semData)
                {
                    semesterList.Add(new Semester_Tbl
                    {
                        Sem_id = int.Parse(i.Sem_id.ToString()),
                        Sem_Name = i.Sem_Name.ToString(),
                    });
                }

                ViewBag.SemData = new SelectList(semesterList, "Sem_id", "Sem_Name");


                var studentVM = new StudentVM
                {
                    Student_id = studentData.Student_id,
                    Student_Code = studentData.Student_Code,
                    Name = studentData.Name,
                    Address = studentData.Address,
                    City = studentData.City,
                    State = studentData.State,
                    PinCode = studentData.PinCode,
                    Mobile = studentData.Mobile,
                    EmailId = studentData.EmailId,
                    college_id = studentData.FKCollege_ID,
                    year_id = studentData.FkYearId,
                    Sem_id = studentData.FKSemID

                };


                return View(studentVM);
            }
        }

        public ActionResult UpdateStudent(StudentVM vm)
        {
            BindStates();
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                

                var data = db.Student_tbl.Where(x => x.Student_id == vm.Student_id).FirstOrDefault();
                data.Student_Code = vm.Student_Code;
                data.Name = vm.Name;
                data.Address = vm.Address;
                data.City = vm.City;
                data.State = vm.State;
                data.PinCode = vm.PinCode;
                data.Mobile = vm.Mobile;
                data.EmailId = vm.EmailId;

                data.FKCollege_ID = vm.college_id;
                data.FkYearId = vm.year_id;
                data.FKSemID = vm.Sem_id;

                db.Student_tbl.Attach(data);
                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Remove(int id)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.Student_tbl.Where(x => x.Student_id == id).FirstOrDefault();
                db.Student_tbl.Remove(data);
                db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        private void BindStates()
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                List<State_List> UKStates = db.State_List.ToList();
                ViewBag.DDLStates = new SelectList(UKStates, "State_Name", "State_Name");
            }
        }

    }
}