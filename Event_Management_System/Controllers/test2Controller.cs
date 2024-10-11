using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Event_Management_System.Controllers
{
    public class test2Controller : Controller
    {
        // GET: test2
        public ActionResult Index()
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    // Set up the email properties
                    mail.From = new MailAddress("arya.gajendra@outlook.com");  // Your Outlook email
                    mail.To.Add("arya.gajendra@gmail.com");  // Recipient's email
                    mail.Subject = "EMS Mail";
                    mail.Body = "This is an EMS email.";

                    // Set up the SMTP client
                    using (SmtpClient smtpServer = new SmtpClient("smtp.office365.com"))
                    {
                        smtpServer.Port = 587;  // Port for SMTP over TLS
                        smtpServer.Credentials = new NetworkCredential("arya.gajendra@outlook.com", "pwfaspvouaqukgts");  // Use the App Password here
                        smtpServer.EnableSsl = true;
                        //smtpServer.EnableSsl = false;  // Disable SSL
                        // Send the email
                        
                        smtpServer.Send(mail);
                        Console.WriteLine("Email Sent Successfully!");
                    }
                }
            }
            catch (SmtpException smtpEx)
            {
                // Handle specific SMTP exceptions
                Console.WriteLine("SMTP Exception: " + smtpEx.Message);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return View();
        }
    }
}