using Event_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using System.Collections;
using System.Runtime.Remoting.Contexts;
using System.Web.UI.WebControls;

namespace Event_Management_System.Controllers
{
    public class EventLocationMasterController : Controller
    {
        // GET: EventLocationMaster
        public ActionResult Index()
        {
            var seats = GenerateSeats(10, 10);
            //UnEqualGenerateSeats();

           // return View(groupedSeats);

            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                List<EventLocationMaster_Tbl> evtLoc = db.EventLocationMaster_Tbl.Include(i => i.College_Tbl).ToList();
                return View(evtLoc);
            }
            
        }

        public ActionResult Add()
        {
            //var seats = GenerateSeats(10, 10); // 10 rows, 10 seats per row
            //var groupedSeats = seats
            //    .GroupBy(s => s.Row)
            //    .ToDictionary(g => g.Key, g => g.ToList());
            ////ViewBag.Seats = groupedSeats;
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

        public JsonResult GetSeatEqual(int numRows, int seatsPerRow)     
        {

            var seats = new List<Seat>();

            for (int rowIndex = 0; rowIndex < numRows; rowIndex++)
            {
                char rowChar = (char)('A' + rowIndex);
                for (int seatNumber = 1; seatNumber <= seatsPerRow; seatNumber++)
                {
                    seats.Add(new Seat
                    {
                        Row = rowChar.ToString(),
                        Number = seatNumber
                    });
                }
            }

            var groupedSeats = seats
                .GroupBy(s => s.Row)
                .ToDictionary(g => g.Key, g => g.ToList());
               ViewBag.Seats = groupedSeats;

            // Return the subcategories as JSON
            return Json(seats, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Book(string selectedSeats)
        {
            if (!string.IsNullOrEmpty(selectedSeats))
            {
                // Split the selected seats by comma
                var seats = selectedSeats.Split(',').ToList();

                // Process the selected seats (e.g., save to database, etc.)
                // For demonstration, we will just return the selected seats to the view
                ViewBag.SelectedSeats = seats;
            }

            return View("BookingConfirmation"); // Or redirect to a confirmation page
        }
        private List<Seat> GenerateSeats(int numRows, int seatsPerRow)
        {
            var seats = new List<Seat>();

            for (int rowIndex = 0; rowIndex < numRows; rowIndex++)
            {
                char rowChar = (char)('A' + rowIndex);
                for (int seatNumber = 1; seatNumber <= seatsPerRow; seatNumber++)
                {
                    seats.Add(new Seat
                    {
                        Row = rowChar.ToString(),
                        Number = seatNumber
                    });
                }
            }

            return seats;
        }
        public JsonResult SaveSeatDetailsEqual(string vCollege, string vLocation, string vtotalSeats, string vnumberOfRows, string vddlSeattype, string rowsvalues)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                EventLocationMaster_Tbl arg = new EventLocationMaster_Tbl();

                arg.Location = vLocation;
                arg.No_of_Seat = int.Parse(vtotalSeats);
                arg.No_Of_Row = int.Parse(vnumberOfRows);
                arg.Seattype = vddlSeattype;

                arg.Created_Date = DateTime.Now;
                arg.Created_by = 1;
                arg.FKCollege_ID = int.Parse(vCollege);
                //arg.rowvalue = rowsvalues;

                db.EventLocationMaster_Tbl.Add(arg);
                db.SaveChanges();
                //return RedirectToAction("Index");


                // Retrieve the generated ID
                int eventLocId = arg.EventLoc_ID; // Assuming 'ID' is the primary key column name



                // var seatRows = UnEqualGenerateSeats(rowsvalues);
                string[] seatRows = rowsvalues.Split(',');

                for (int i = 0; i < seatRows.Length; i++)
                {
                    string[] seat1 = seatRows[i].Split('-');
                    var seat = new Seat_tbl
                    {
                        Seat_Name = seat1[1].ToString(), // Assign the first part to Seat_Name
                        Seat_row = seat1[0].ToString(),   // Assign the second part to Seat_row
                        FK_EventLoc_ID = eventLocId,
                        ISAVAIL = 0
                    };

                    // Add the object to the DbSet
                    db.Seat_tbl.Add(seat);

                }
               
                db.SaveChanges();

            }

            // Return the subcategories as JSON
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveSeatDetails(string vCollege,string vLocation,string vtotalSeats,string vnumberOfRows,string vddlSeattype, string rowsvalues)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                EventLocationMaster_Tbl arg = new EventLocationMaster_Tbl();

                    arg.Location = vLocation;
                    arg.No_of_Seat = int.Parse(vtotalSeats);
                    arg.No_Of_Row = int.Parse(vnumberOfRows);
                    arg.Seattype = vddlSeattype;

                    arg.Created_Date = DateTime.Now;
                    arg.Created_by = 1;
                    arg.FKCollege_ID =int.Parse(vCollege);
                    arg.rowvalue = rowsvalues;

                    db.EventLocationMaster_Tbl.Add(arg);
                    db.SaveChanges();
                //return RedirectToAction("Index");
               

                // Retrieve the generated ID
                int eventLocId = arg.EventLoc_ID; // Assuming 'ID' is the primary key column name



                var seatRows = UnEqualGenerateSeats(rowsvalues);

                foreach (var item in seatRows)
                {
                    
                    foreach (var item1 in item)
                    {
                        string[] parts = item1.Split('-');
                        //db.Seat_tbl.Add(new Seat_tbl { Seat_Name = parts[0].ToString() });
                        //db.Seat_tbl.Add(new Seat_tbl { Seat_row = parts[1].ToString() });
                        var seat = new Seat_tbl
                        {
                            Seat_Name = parts[0], // Assign the first part to Seat_Name
                            Seat_row = parts[1],   // Assign the second part to Seat_row
                            FK_EventLoc_ID=eventLocId,
                            ISAVAIL =0
                        };

                        // Add the object to the DbSet
                        db.Seat_tbl.Add(seat);

                    }
                }
                db.SaveChanges();

            }

            // Return the subcategories as JSON
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddEvtLocation(EvtLocationCollegeVM vm)
        {
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                EventLocationMaster_Tbl arg = new EventLocationMaster_Tbl();

                arg.Location = vm.Location;
                arg.No_of_Seat = vm.No_of_Seat;
                arg.No_Of_Row = vm.No_Of_Row;
                arg.Seattype=vm.Seattype;

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

        public List<List<string>> UnEqualGenerateSeats(string rowsvalues)
        {
            var seatRows = new List<List<string>>();

            // Given comma-separated string (or you might get this from the model or another source)
           // rowsvalues = "10,8,10,8,4,10,10,10,15,15";

            // Convert comma-separated string to a list of integers
            List<int> seatsPerRow = rowsvalues
                .Split(',')                // Split the string by commas
                .Select(s => int.Parse(s)) // Convert each substring to an integer
                .ToList();

            // Generate seats based on the input
            for (int i = 0; i < seatsPerRow.Count; i++)
            {
                var rowSeats = new List<string>();
                for (int j = 0; j < seatsPerRow[i]; j++)
                {
                    char rowLetter = (char)('A' + i);
                    rowSeats.Add($"{rowLetter}{j + 1}{"-"}{i+1}");
                }
                seatRows.Add(rowSeats);
            }

            // Return the list of seat rows
            return seatRows;
        }

    }
}