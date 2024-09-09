using Event_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using ZXing;
using System.Net.Sockets;

namespace Event_Management_System.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket
        public ActionResult Index()
        {

            int vbookingid = int.Parse(TempData["Bookingid"].ToString());
            var bookinginfo = new List<bookinginfo>();
            var ticket = new List<Ticket>();
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {

                var bookings = db.BookingInfo_tbl
         .Include(b => b.Booking_SeatInfo_tbl)
         .Include(b => b.Event_Tbl)
         .Include(b => b.EventLocationMaster_Tbl)
         .Include (b => b.Student_tbl)
         .Where(b => b.Booking_id == vbookingid)
         .ToList();

                var result = new List<Ticket>();

                foreach (var booking in bookings)
                {
                    var seats = booking.Booking_SeatInfo_tbl.Select(bs => bs.Seat_tbl.Seat_Name).ToList();
                    var seatsrow = booking.Booking_SeatInfo_tbl.Select(bs => bs.Seat_tbl.Seat_row).ToList();
                    //  string bookingSeats = string.Join(",", seats);
                    // Combine rows and seats
                    var combinedSeats = seatsrow.Zip(seats, (row, seat) => $"{row}/{seat}").ToList();

                    // Join combined seats into a single string
                    string formattedSeats = string.Join(",", combinedSeats);

                    //  var Eventmst = db.EventMaster_Tbl.Include(acb => acb.Event_Tbl).Where(x=>x.Event_Tbl.FK_Event_Mst_Id=booking.Event_Tbl.Event_Id).ToList();
                    var Eventmst = db.EventMaster_Tbl
          .Where(em => em.Event_Tbl.Any(et => et.Event_Id == booking.Event_Tbl.Event_Id))
          .ToList();
                    // Generate the QR code image
                    var qrCodeImage = QRCodeGenerator.GenerateQRCode(new
                    {
                        BookingId = booking.Booking_id,
                        Seats = formattedSeats,
                        EventName = Eventmst[0].Event_Name,
                        EventDate = booking.Event_Tbl.Event_FromDate.ToString("yyyy-MM-dd"),
                        EventTime = $"{booking.Event_Tbl.Event_FromTime} - {booking.Event_Tbl.Event_ToTime}",
                        StudentName=booking.Student_tbl.Name,
                        CollegeName = booking.Student_tbl.College_Tbl.College_Name,
                        Venue = booking.EventLocationMaster_Tbl.Location,
                        TicketPrice=booking.TotalPrice.ToString()
                    });

                    // Convert QR code image to base64 string
                    var qrCodeBase64 = Convert.ToBase64String(qrCodeImage);

                    //result.Add(new Ticket
                    //{
                    //    BookingId = booking.Booking_id,
                    //    Seats = bookingSeats,
                    //    EventName = "Gaj",
                    //    EventDate = booking.Event_Tbl.Event_FromDate.ToString("yyyy-MM-dd"),
                    //    EventTime = $"{booking.Event_Tbl.Event_FromTime} - {booking.Event_Tbl.Event_ToTime}",
                    //    Venue = booking.EventLocationMaster_Tbl.Location,
                    //    QRCodeBase64 = $"data:image/png;base64,{qrCodeBase64}"
                    //});
                    ticket.Add( new Ticket
                    {
                        BookingId = booking.Booking_id,
                        Seats = formattedSeats,
                        EventName = Eventmst[0].Event_Name,
                        // EventDate = booking.Event_Tbl.Event_FromDate.ToString("yyyy-MM-dd"),
                        EventDate = booking.Event_Tbl.Event_FromDate.ToString("dd-MM-yyyy"),
                        EventTime = $"{booking.Event_Tbl.Event_FromTime} - {booking.Event_Tbl.Event_ToTime}",
                        Venue = booking.EventLocationMaster_Tbl.Location,
                        StudentName = booking.Student_tbl.Name,
                        CollName = booking.Student_tbl.College_Tbl.College_Name,
                        QRCodeBase64 = $"data:image/png;base64,{qrCodeBase64}",
                        TicketPrice = booking.TotalPrice.ToString(),
                    });

                   
                }
                return View(ticket);
                // result.FirstOrDefault(); // Assuming you want the first result

                //  return View(result.FirstOrDefault());
            }
        }
    }

}
