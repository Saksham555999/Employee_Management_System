using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness_Entities;
using DAL;
namespace BLOGIC
{
    public class blogic
    {
        dal obj = new dal();
        public void addcourses(string coursename, DateTime startdate, DateTime enddate, string desc)
        {
            
            if((startdate>DateTime.Now && enddate>DateTime.Now) && enddate>startdate )
            {
                if (obj.coursenamealreadypresent(coursename))
                {
                    obj.addcourses(coursename, startdate, enddate, desc);
                }
                else
                {
                    obj.addcoursenameifnotpresent(coursename);
                    obj.addcourses(coursename, startdate, enddate, desc);
                }
            }
           
            else
            {
                throw new Exception("Enter Valid Dates");
            }
        }

        public List<Bussiness_Entities.Feedback> Displayfeedback(string dropdown, string value)
        {
            return obj.Displayfeedback(dropdown, value);
        }
        public string getemail(string employeeid)
        {
             return  obj.getemail(employeeid);
        }

        public List<Bussiness_Entities.Employeetrainingnew> displayregisteredtrainings()
        {
            return obj.displayregisteredtrainings();
        }
        public List<Bussiness_Entities.Training> displayongoingtraining()
        {
            return obj.displayongoingtraining();
        }
        public List<Bussiness_Entities.Coursename> displaycoursename()
        {
            return obj.displaycoursename();
        }

        
            public int Feedbackform(string empid, string courseid, string Description,string q1, string q2, string q3, string q4, string q5)
            {
                return obj.Feedbackform(empid, courseid, Description,q1,q2,q3,q4,q5);
            }
        

        public List<Bussiness_Entities.Attendance> EmployeeAttendance(DateTime Date)
        {
            return obj.EmployeeAttendance(Date);
        }

        

        public int removecourse(int coursenameid,int courseid)
        {
           return obj.removecourse(coursenameid,courseid);
        }

        public string getmanagername(string managerid)
        {
           return obj.getmanagername(managerid);
        }

        public int Register(string employeeid, string courseid)
        {
            try
            {
                if (obj.courseisstartedorended(courseid))
                {
                    if (obj.checkdateclashforemployee(employeeid,courseid))
                    {
                        return obj.Register(employeeid, courseid);
                    }
                   else
                    {
                        throw new Exception("Complete Ongoing Courses Before Registring for new One's");
                    }
                    
                }
                else
                {
                    throw new Exception("Registration should be one day done prior to startdate");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
            
        }

        public string getHRid(string hrname)
        {
            return obj.getHRid(hrname);
        }
        public string getManagerid(string managername)
        {
            return obj.getManagerid(managername);
        }
        public string getEmployeeid(string hrname)
        {
            return obj.getEmployeeid(hrname);
        }

        public int HRLogin(string userid, string password)
        {
            int i= obj.HRLogin(userid, password); 
            if(i>0)
            {
                return i;
            }
            else
            {
                throw new Exception("invalid credentials");
            }
        }

        public int HRRegistration(string hrname, string password, string mailid)
        {
            return obj.HRRegistration(hrname, password, mailid);

        }

        public object EmployeeLogin(string userid, string password)
        {
            try
            {
                return obj.EmployeeLogin(userid, password);
            }
            catch
            {
                throw new Exception("invalid credentials");
            }
        }

        public List<Bussiness_Entities.Employeetrainingnew> Statusoftraining(string empid)
        {
            return obj.statusoftraining(empid);
        }

        public int EditDetails(string employeeid, string emailid, string phonenumber, string address)
        {
            return obj.EditDetails(employeeid, emailid, phonenumber, address);
        }

        public int canceltraining(string empid,int courseid)
        {
           return obj.canceltraining(empid,courseid);
        }

        public int ManagerRegistration(string managername, string password, string mailid, string deptname)
        {
            return obj.ManagerRegistration(managername, password, mailid, deptname);
        }
        public void Approve(string Status,string employeeid,string reason)
        {
            obj.Approve(Status,employeeid,reason);
        }

        public void Reject(string Status,string employeeid)
        {
            obj.Reject(Status,employeeid);
        }
        public void removeattendence(string employeeid,int courseid)
        {
            obj.removeattendence(employeeid, courseid);
        }

        public object ManagerLogin(string userid, string password)
        {
            var i=obj.ManagerLogin(userid, password);
            if((int)i>0)
            {
                return i;
            }
            else
            {
                throw new Exception("invalid credentials");
            }
        }

        public int EmployeeRegistration(string username, string password, string manid, string HRid, string address, string mailid, string phonenumber)
        {
            return obj.EmployeeRegistration(username, password, manid, HRid, address, mailid, phonenumber);
        }
        public int Giveattendance(string employeeid, int courseid)
        {
            if (obj.employeeintraining(employeeid))
            {
                if (obj.approvedbymanager(employeeid))
                {
                    try
                    {
                        return obj.Giveattendance(employeeid, courseid);
                    }
                    catch
                    {
                        throw new Exception("Cannot Mark Attendence Twice");
                    }
                }
                else
                {
                    throw new Exception("Cannot Mark Attendence without Manager Approval");
                }
            }
            return 0;
        }
    }
}
