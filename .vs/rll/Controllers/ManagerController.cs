using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bussiness_Entities;
using BLOGIC;
namespace rll.Controllers
{
    public class ManagerController : Controller
    {
        blogic obj = new blogic();
        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Approval()
        {
            
                return View(obj.displayregisteredtrainings());
            
            

        }
        public ActionResult feedback()
        {
            try
            {
                string employeeid = Request.QueryString["employeeid"].ToString();
                Session["employeeemail"] = obj.getemail(employeeid);
                
            }
            catch(Exception e)
            {
                ViewData["error"] = e.Message;
            }

            return RedirectToAction("Index", "sendmail");

        }
        public ActionResult Approve(string reason)
        {
            try
            {
                string status = Request.QueryString["Status"].ToString();
                string employeeid = Request.QueryString["employeeid"].ToString();
                obj.Approve(status, employeeid, reason);
            }
            catch(Exception e)
            {
                ViewData["error"] = e.Message;
            }
            return RedirectToAction("Approval");
        }
        [HttpGet]
        public ActionResult Displayfeedback()
        {
            try
            {
                ViewBag.b = obj.displaycoursename();
                ViewBag.a = null;
            }
            catch (Exception e)
            {
                ViewData["error"] = e.Message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Displayfeedback(string dropdown, string value)
        {
            try
            {
                ViewBag.b = obj.displaycoursename();
                ViewBag.a = obj.Displayfeedback(dropdown, value);
            }
            catch (Exception e)
            {
                ViewData["error"] = e.Message;
            }

            return View();
        }
            public ActionResult Welcome()
        {
            try
            {
                ViewBag.a = obj.getmanagername(Session["Manager"].ToString());
            }
            catch (Exception e)
            {
                ViewData["error"] = e.Message;
            }
            return View();
        }
        
        public ActionResult EmployeeAttendance()
        {
            ViewBag.a = null;
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeAttendance(DateTime Date)
        {   try
            {
                ViewBag.a = obj.EmployeeAttendance(Date);
            }
            catch (Exception e)
            {
                ViewData["error"] = e.Message;
            }
            return View();
        }

        public ActionResult Reject()
        {
            string status = Request.QueryString["Status"].ToString();
            string employeeid = Request.QueryString["employeeid"].ToString();
            obj.Reject(status,employeeid);
            return RedirectToAction("Approval");
        }
        public ActionResult logout()
        {
            return RedirectToAction("Login","Home");
        }
    }
}