using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLOGIC;
using Bussiness_Entities;
namespace rll.Controllers
{
    public class HomeController : Controller
    {
        blogic obj = new blogic();
        
        // GET: Home
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string userid, string password, string dropdown)
        {
            try
            {
                if (dropdown == "HR")
                {
                    int res = obj.HRLogin(userid, password);
                    if ((int)res == 1)
                    {
                        Session["hr"] = userid;
                        return RedirectToAction("addcourses","HR");
                        //ViewData["a"] = " HR Login Successfull";
                        
                    }
                }
                else if (dropdown == "Employee")
                {   
                    var res = obj.EmployeeLogin(userid, password);
                    if ((int)res == 1)
                    {
                        Session["Employee"] = userid;
                        return RedirectToAction("Registerfortraining","Employee");
                    }
                }
                else if (dropdown == "Manager")
                {
                    var res = obj.ManagerLogin(userid, password);
                    if ((int)res == 1)
                    {
                        Session["Manager"] = userid;
                       // ViewData["c"] = "Manager Login Successfull";
                        return RedirectToAction("Welcome", "Manager");
                    }
                }
            }
            catch (Exception e)
            {
                ViewData["a"] = e.Message;
            }
            return View();
            //[HttpPost]
            //public ActionResult Login()
            //{
            //    return View();
            //}
        }
        public ActionResult HRRegistration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult HRRegistration(string hrname, string password, string mailid)
        { 
            try
            {
                int i = obj.HRRegistration(hrname, password, mailid);
                if (i > 0)
                {
                    ViewData["b"] = "HR Id is:"+obj.getHRid(hrname);
                    ViewData["a"] = "HR added Successfully";
                }
            }
            catch(Exception e)
            {
                ViewData["error"] = e.Message;
            }
            return View();
        }
        public ActionResult ManagerRegistration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ManagerRegistration(string managername, string password, string mailid, string deptname)
        {
            try
            {
                int i = obj.ManagerRegistration(managername, password, mailid, deptname);
                if (i > 0)
                {
                    ViewData["b"] = "Manager Name is: "+obj.getmanagername(managername);
                    ViewData["c"] = "Manager ID is: " +obj.getManagerid(managername);
                    ViewData["a"] = "Manager added Successfully";
                }
            }
            catch(Exception e)
            {
                ViewData["error"] = e.Message;
            }
            return View();
        }

        public ActionResult EmployeeRegistration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeRegistration(string username, string password, string manid, string HRid, string address, string mailid, string phonenumber)
        {
            try
            {
                int i = obj.EmployeeRegistration(username, password, manid, HRid, address, mailid, phonenumber);
                if (i > 0)
                {
                    ViewData["b"] = " Your Employee id : "+obj.getEmployeeid(username);
                    ViewData["a"] = "Employee Registered Successfully";
                }
            }
            catch(Exception e)
            {
                ViewData["error"] = "Invalid HR or Manager ID";
            }
            return View();
        }
    }
}