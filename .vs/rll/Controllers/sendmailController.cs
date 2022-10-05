using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using BLOGIC;
namespace rll.Controllers
{
    public class sendmailController : Controller
    {
        blogic obj = new blogic();
        // GET: sendmail
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string toaddress, string uname, string pwd)
        {
            string toaddress1 = Session["employeeemail"].ToString();
            MimeMessage email = new MimeMessage();
            email.Sender = MailboxAddress.Parse("saksham.batheja37@gmail.com");
            email.To.Add(MailboxAddress.Parse(toaddress1));
            email.Subject = "Feedback Form";
            email.Body = new TextPart(TextFormat.Html) { Text = "<h1>https://localhost:44324/sendmail/Feedbackform?empid=&empid=&q1=&q2=&q3=&q4=&q5=&q6=</h1>" };

            // send email
            SmtpClient smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(uname, pwd);
            smtp.Send(email);
            smtp.Disconnect(true);
            return View();
        }
        public ActionResult Feedbackform()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Feedbackform(string empid, string courseid, string Description,string q1, string q2, string q3, string q4, string q5)
        {
            int i = obj.Feedbackform(empid, courseid, Description,q1,q2,q3,q4,q5);
            if (i > 0)
            {
                ViewData["a"] = "Form Submitted Successfully";
            }
            else
            {
                ViewData["b"] = "Entrered Details are not found";
            }

            return View();
        }
    }
}