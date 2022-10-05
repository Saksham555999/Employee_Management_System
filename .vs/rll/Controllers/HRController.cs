using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bussiness_Entities;
using BLOGIC;
namespace rll.Controllers
{
    public class HRController : Controller
    {
        blogic obj = new blogic();
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Home");
           
        }
        public ActionResult addcourses()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult addcourses(string coursename,DateTime startdate,DateTime enddate,string desc)
        {
            try
            {
               
                
                    obj.addcourses(coursename, startdate, enddate, desc);
                
            }
            catch(Exception e)
            {
                ViewData["message"] = e.Message;
            }
            return View();
        }
        public ActionResult displaytraining()
        {
            
            ViewBag.table1= obj.displayongoingtraining();
            ViewBag.table2 = obj.displaycoursename();
            
            return View();
        }
        public ActionResult remove(string coursenameid,string courseid)
        {
            try
            {
                int x = obj.removecourse(Int32.Parse(coursenameid), Int32.Parse(courseid));
                if (x > 0)
                {
                    return RedirectToAction("displaytraining");
                }
            }
            catch(Exception e)
            {
                Session["error123"] = e.Message;
            }
            return View();
        }
    }
}
