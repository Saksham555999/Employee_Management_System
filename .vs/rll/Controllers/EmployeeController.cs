using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLOGIC;
using Bussiness_Entities;
namespace rll.Controllers
{
    
    public class EmployeeController : Controller
    {
        blogic obj = new blogic();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register(int courseid)
        {
            TempData["courseid"] = courseid;
            ViewData["status"] = "Not Registered";
            return View();
        }
        [HttpPost]
        public ActionResult Register(string employeeid,string courseid)
        {
            try
            {
                int i = obj.Register(employeeid, courseid);
                if (i > 0)
                {
                    ViewData["status"] = "Wait for Manager approval";
                }
            }
            catch(Exception e)
            {
                ViewData["status"] = e.Message;
            }
            return View();
        }
        public ActionResult Attendence()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Attendence(string Employeeid, int Courseid)
        {
            try
            {
                int x = obj.Giveattendance(Employeeid, Courseid);
                if (x == 0)
                {
                    ViewData["a"] = "Employee is not in Training List";
                }
                else if (x == 1)
                {
                    ViewData["a"] = "Attendence Marked";
                }
            }
            catch(Exception e)
            {
                ViewData["a"] = e.Message;
            }
            return View();
        }

        public ActionResult Editpersonaldetails()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Editpersonaldetails(string Employeeid, string Emailid, string Phonenumber, string Address)
        {
            try
            {
                int i = obj.EditDetails(Employeeid, Emailid, Phonenumber, Address);
                if (i > 0)
                {
                    ViewData["status"] = "details updated successfully";
                }
            }
            catch(Exception e)
            {
                ViewData["error"] = e.Message;
            }
            return View();
        }
        public ActionResult Statusoftraining()
        {
            return View(obj.Statusoftraining(Session["Employee"].ToString()));
        }
        public ActionResult cancel()
        {
            try
            {
                string empid = Session["Employee"].ToString();
                int courseid = Int32.Parse(Request.QueryString["courseid"].ToString());
                int i = obj.canceltraining(empid, courseid);
                if (i > 0)
                {
                    // obj.removeattendence(empid,courseid);

                    return RedirectToAction("Statusoftraining");
                }
            }
                 catch (Exception e)
            {
                ViewData["error"] = e.Message;
            }
        
            return View();
        }
        
        public ActionResult Registerfortraining()
        {
            
                return View(obj.displayongoingtraining());
            
            
        }
       public ActionResult Logout()
        {
           
            return RedirectToAction("Login", "Home");
            
        }
        
    }
}