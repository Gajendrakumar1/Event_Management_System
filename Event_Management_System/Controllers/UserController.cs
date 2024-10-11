using Event_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Event_Management_System.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            // Check if the user is authenticated
            //if (Session["Type"] == null)
            //{
            //    return RedirectToAction("Index", "SignIn");
            //}
            BindStates();
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                var data = db.User_tbl.ToList();
                return View(data);
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

        public ActionResult Create()
        {
            BindStates();
            string mobileSession = Session["Mobile"] as string;
            ViewBag.mobileNumData = mobileSession;

            return View();
        }


        // it will call on click event
        public ActionResult AddCollege(ColgUser c,string startDate, string endDate)
        {

//            string name = "Gaj";
//           string email = "arya.gajendra@gmail.com";
//                var message = new MimeMessage();
//                message.From.Add(new MailboxAddress("EmS", "arya.gajendra@outlook.com"));
//                message.To.Add(new MailboxAddress(name, email));
//                message.Subject = "Welcome to [Your Event Management App Name]!";

//                message.Body = new TextPart("plain")
//                {
//                    Text = $@"Hi {name},

//Welcome to [Your Event Management App Name]! We are thrilled to have you on board.

//[Your email body here]
//"
//                };

//            //using (var client = new SmtpClient())
//            //{
//            //    client.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
//            //    client.Authenticate("your_email@outlook.com", "your_password"); // Aapka Outlook password
//            //    client.Send(message);
//            //    client.Disconnect(true);
//            //}

//            using (var client = new SmtpClient())
//            {
//                client.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
//                client.Authenticate("arya.gajendra@outlook.com", "Gajju@2024"); // Aapka Outlook password
//                client.Send(message);
//                client.Disconnect(true);
//            }




            BindStates();
            using (Event_Management_SystemEntities db = new Event_Management_SystemEntities())
            {
                College_Tbl col = new College_Tbl
                {
                    Created_Date = DateTime.Now,
                    Created_by = 1, // Make sure to adjust this as needed
                    College_Name = c.College_Name.Trim(),
                    Address = c.Address.Trim(),
                    City = c.City.Trim(),
                    State = c.State.Trim(),
                    PinCode = c.PinCode.Trim(),
                    InitialName = c.InitialName.Trim()
                };

                // Add the College_Tbl instance instead of the ColgUser instance
                db.College_Tbl.Add(col);
                db.SaveChanges();
                // Retrieve the ID of the newly created college
                int collegeId = col.College_id; // Replace 'CollegeID' with your actual primary key property name

                User_tbl u = new User_tbl
                {
                    Name = c.Name,
                    mobileNo = c.mobileNo,
                    EmailId = c.EmailId,
                    CollegeID = collegeId,
                    EmpCode = c.EmpCode.Trim(),
                    AccessFromdate = DateTime.Parse(startDate),
                    AccessTodate = DateTime.Parse(endDate),


                    Period = c.Period.Trim(),
                    createdon = DateTime.Now,
                    createdby = 1,
                    isactive=1,
                    isdeleted=0
                };
                // Add the User_tbl instance instead of the ColgUser instance
                db.User_tbl.Add(u);
                db.SaveChanges();

                //                var fromAddress = new MailAddress("kumargaj70@gmail.com", "EMS");
                //                var toAddress = new MailAddress(c.EmailId, c.Name);
                //                const string fromPassword = "Gajju@20241234";
                //                const string subject = "Welcome to EMS!";

                //                string body = $@"
                //          <center style=""width:100%;table-layout:fixed;background-color:#fff;max-width:700px;margin:0 auto"">
                //        <div style=""max-width:600px;padding-bottom:10px;border:10px solid #fbf3f3;background:#fff;margin:0 auto 0 auto"">
                //            <table  style=""max-width:600px"" width=""100%"" cellspacing=""0"" cellpadding=""0"" align=""center""><tbody><tr><td><div style=""max-width:600px;width:100%;text-align:center;padding:10px 0 0!important"">
                //    <a href=""#""><img src=""../Content/Login/EMS-logo.jpeg"" alt=""Logo"" width=""150"" height=""100""  data-bit=""iit""></a>
                //    <br>
                //</div></td></tr><tr><td style=""text-align:left;vertical-align:middle;padding:0 20px 20px;margin:0;font-family:'Open Sans',Arial,sans-serif;font-size:14px;color:#444"">
                //    <p style=""height:17px;border-top:1px solid #ffbfc1;margin-top:10px!important""><br></p>
                //    <p style=""line-height:24px;margin:10px 0!important;font-size:16px"">Dear <span style=""color:#333;font-weight:bold"">@"+c.Name+@" ,</span></p>
                //    <p style=""line-height:24px;margin:10px 0!important"">Welcome to EMS! We are thrilled to have you on board.</p>
                //    <p style=""line-height:24px;margin:10px 0!important"">
                //        At EMS, we strive to provide you with the best event management experience. Whether you’re planning a small gathering or a large conference, we’ve got you covered with our tools and resources.
                //    </p>
                //    <p style=""line-height:24px;margin:10px 0!important"">
                //        Here’s what you can do next:
                //    </p>
                //    <p style=""line-height:24px;margin:10px 0!important"">
                //        - Explore our features: Check out our dashboard to see all the tools available to you.
                //    </p>
                //    <p style=""line-height:24px;margin:10px 0!important"">
                //        - Join our community: Connect with other event planners and share your experiences.
                //    </p>
                //    <p style=""line-height:24px;margin:10px 0!important"">    - Need help? Our support team is here to assist you anytime.</p>


                //    <p style=""line-height:24px;margin:10px 0!important"">Please consider taking 2 minutes to review us.</p>
                //    <p style=""line-height:24px;margin:20px 0 10px!important"">
                //        <a href=""#"" style=""text-decoration:none;display:inline-block;padding:2px 20px;border:1px solid #c10504;color:#c10504;font-size:16px;border-radius:2px"" target=""_blank"">Share Your Feedback</a>
                //        <br>
                //    </p>

                //    <p style=""line-height:20px;margin:10px 0!important"">
                //        Thank you for joining us! We look forward to helping you create amazing events.

                //    </p>
                //    <p style=""line-height:20px;margin:20px 0 10px!important;font-size:15px;font-weight:bold"">Best Regards,</p>
                //    <p style=""line-height:20px;margin:2px 0!important;font-size:14px"">The EMS Team</p>
                //</td></tr></tbody></table>


                //            <table  style=""max-width:600px;background:#fafaf8;margin:0px auto;height:76px;width:100%"" width=""100%"" cellspacing=""0"" cellpadding=""0"" align=""center"">
                //                <tbody>
                //                    <tr style=""height:22px"">
                //                        <td style=""height:22px"">
                //                            <table style=""max-width:600px;background:#fafaf8;margin:0 auto"" width=""100%"" cellspacing=""0"" cellpadding=""0"" align=""center"">
                //                                <tbody>
                //                                    <tr>
                //                                        <td height=""10"">&nbsp;</td>
                //                                    </tr>
                //                                </tbody>
                //                            </table>
                //                        </td>
                //                    </tr>
                //                    <tr>
                //                        <td>
                //                            <p style=""padding:10px!important;color:#444;font-size:14px;margin-bottom:5px!important;text-align:center;border-bottom:1px solid #ccc"">Can we help you?</p>
                //                        </td>
                //                    </tr>
                //                    <tr>
                //                        <td style=""padding:5px 0px!important;text-align:center"">
                //                            <p style=""padding:5px 0;font-size:12px;width:50%;float:left""><img style=""vertical-align:middle;padding-right:5px"" src=""https://ci3.googleusercontent.com/meips/ADKq_Naqt4RdB7fW3TitFgNYt7WQnhcNiu6LqjBb38C8Z5RqTXWv9tIzL_dc7M4pOLnAfKwpem0P2k2sDg34j6wIbpPQe-VZRsBOqoMopJJpucR88Ja3T5VH6V36ytGlj8iD6YSpLm_mok8eghbS8FgHtRDsM-0A_z1vTN5wCAKY6FWDhQM-dEAroJU=s0-d-e1-ft#https://fagift-marketing.s3.ap-south-1.amazonaws.com/Floweraura-Retention/Vaishali/footers/social+icons/mobile.jpg"" alt=""mobile""  data-bit=""iit""><a style=""color:#464646;padding:0 2px;text-decoration:none;font-size:13px;line-height:15px"" href=""tel:08882553333"" target=""_blank""> +91-9971989212</a></p>
                //                            <p style=""padding:5px 0;font-size:12px;width:50%;float:left""><img style=""vertical-align:middle;padding-right:5px"" src=""https://ci3.googleusercontent.com/meips/ADKq_NaXH6r0cLv8c4zjK7w0xFX1j_slOLwTMjWIPPJWUomXhgIWzmyULypUHTO_9K7MNJkOIaB1bOTCHmGSgjIm3d1zJNLVTIt-0lNVFFxtdvKfPzsjgfj7DsBbjo-ZFV4xqTCKcUcvmy5yWkPfdPLnvECk3d-urdkYabmeHLuIR-SLtk2YiHN1=s0-d-e1-ft#https://fagift-marketing.s3.ap-south-1.amazonaws.com/Floweraura-Retention/Vaishali/footers/social+icons/mail.jpg"" alt=""mail""  data-bit=""iit""><a style=""color:#464646;padding:0 2px;text-decoration:none;font-size:13px;line-height:15px"" href=""mailto:care@bakingo.com"" target=""_blank"">care@ems.com</a></p>
                //                        </td>
                //                    </tr>
                //                </tbody>
                //            </table>
                //            <table style=""max-width:600px;line-height:14px;text-align:center;font-family:arial;margin:0px auto;font-size:12px"" width=""100%"" cellspacing=""0"" cellpadding=""0"" align=""center"">
                //                <tbody>
                //                    <tr>
                //                        <td style=""text-align:center"">
                //                            <div style=""font-family:Arial,sans-serif;font-weight:normal;padding:5px;text-align:justify;font-size:11px;line-height:14px;color:#777;background-color:#fff"">Our innovative Event Management System (EMS), designed specifically for colleges to simplify the process of creating and managing events. Whether it’s a cultural fest, workshop, seminar, or any special occasion, our platform makes it easy for college administrators to organize unforgettable experiences.</div>
                //                        </td>
                //                    </tr>
                //                </tbody>
                //            </table>
                //        </div>
                //    </center>
                //        ";

                //                var smtp = new SmtpClient
                //                {
                //                    Host = "smtp.gmail.com", // Aapka SMTP server
                //                    Port = 587, // Port number
                //                    EnableSsl = true,
                //                    //DeliveryMethod = SmtpDeliveryMethod.Network,
                //                    DeliveryMethod = SmtpDeliveryMethod.Network,
                //                    UseDefaultCredentials = false,
                //                    Credentials = new NetworkCredential("kumargaj70@gmail.com", "Gajju@20241234")
                //                };

                //                using (var message = new MailMessage(fromAddress, toAddress)
                //                {
                //                    Subject = subject,
                //                    Body = body,
                //                    IsBodyHtml = true // Agar aap HTML email bhejna chahte hain, toh true kar sakte hain
                //                })
                //                {
                //                    smtp.Send(message);
                //                }
                Session["Collegeid"] = collegeId;
                return RedirectToAction("Index","College");
            }
        }

    }
}