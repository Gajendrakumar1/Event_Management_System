using Event_Management_System.Models;
using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Event_Management_System.Controllers
{
    public class Test1Controller : BaseController
    {
        // GET: Test1
        public Test1Controller(MenuService menuService) : base(menuService)
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
                        smtpServer.Credentials = new NetworkCredential("arya.gajendra@outlook.com", "zikgiezjohqiesii");  // Use the App Password here
                        smtpServer.EnableSsl = true;
                        //smtpServer.EnableSsl = false;  // Disable SSL
                        // Send the email
                        smtpServer.Timeout = 30000;
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
          
            //try
            //{
            //    // Create a new MailMessage object
            //    MailMessage mail = new MailMessage();
            //    mail.From = new MailAddress("arya.gajendra@outlook.com");  // Use your Outlook email
            //    mail.To.Add("kumargaj70@gmail.com");  // Recipient's email
            //    mail.Subject = "EMS Mail";
            //    mail.Body = "This is a EMS email.";

            //    // Set up SMTP client
            //    SmtpClient smtpServer = new SmtpClient("smtp.office365.com");
            //    smtpServer.Port = 587; // Port 587 for SMTP over TLS/SSL
            //    smtpServer.Credentials = new NetworkCredential("arya.gajendra@outlook.com", "flufrjhganoxhfgu");  // Use the App Password here
            //    smtpServer.EnableSsl = true;

            //    // Send the email
            //    smtpServer.Send(mail);
            //    return Content("Email Sent Successfully!");
            //    // status.Text = "Message was sent successfully";
            //}
            //catch (Exception ex)
            //{
            //   // status.Text = ex.StackTrace;
            //}
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}