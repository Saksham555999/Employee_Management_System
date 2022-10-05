using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Bussiness_Entities;
namespace DAL
{
    public class dal
    {
        RLL2Entities db = new RLL2Entities();
        Training t = new Training();
        Coursename c = new Coursename();
        Employeetrainingnew emptrain = new Employeetrainingnew();
        Attendance a = new Attendance();
        HR h = new HR();

        public bool coursenamealreadypresent(string coursename)
        {
            var res = from t in db.Coursenames
                      where t.Coursename1 == coursename
                      select t;
            if (res.Count() > 0)
            {
                return true;
            }
            return false;

        }
        public void addcoursenameifnotpresent(string coursename)
        {
            c.Coursename1 = coursename;
            db.Coursenames.Add(c);
            db.SaveChanges();
        }
        public void addcourses(string coursename, DateTime startdate, DateTime enddate, string desc)
        {
            db.SaveChanges();
            var res = (from t in db.Coursenames
                       where t.Coursename1 == coursename
                       select t.Coursenameid).FirstOrDefault();
            t.Coursenameid = res;
            t.Startdate = startdate;
            t.Enddate = enddate;
            t.Description = desc;
            db.Trainings.Add(t);
            db.SaveChanges();
        }

        public string getemail(string employeeid)
        {
            var res = (from t in db.Employees
                       where t.Employeeid == employeeid
                       select t).FirstOrDefault();
            return res.Emailid;
        }

        public int Feedbackform(string empid, string courseid, string Description, string q1, string q2, string q3, string q4, string q5)
        {
            int c = Int16.Parse(courseid);
            int x1 = Int16.Parse(q1);
            int x2 = Int16.Parse(q2);
            int x3 = Int16.Parse(q3);
            int x4 = Int16.Parse(q4);
            int x5 = Int16.Parse(q5);
            var res = from t in db.Employeetrainingnews
                      where t.Employeeid == empid && t.Courseid == c
                      select t;
            if (res.Count() > 0)
            {
                String s = string.Format($"Insert into Feedback (Employeeid,Courseid,Description,Question1,Question2,Question3,Question4,Question5)values('{empid}',{c},'{Description}',{x1},{x2},{x3},{x4},{x5})");
                return db.Database.ExecuteSqlCommand(s);
            }
            else
            {
                return 0;
            }
        }



        public bool checkdateclashforemployee(string employeeid, string courseid)
        {
            int x = Int32.Parse(courseid);
            {
                try
                {
                    var res = (from t in db.Employeetrainingnews
                               where /* t.Courseid == x && */ t.Employeeid == employeeid
                               select t).FirstOrDefault();

                    //check whether employee having overlaping date trainings
                    if (res != null)
                        return Employee_cannot_have_overlaping_date_course(employeeid, courseid);
                    else
                        throw new Exception("returning true");
                }
                catch(Exception e)
                {
                    return true;
                }

                return false;
            }
        }

        public bool Employee_cannot_have_overlaping_date_course(string employeeid,string courseid)
        {
            int x = Int32.Parse(courseid);
            
                var res1 = (from t in db.Trainings
                            join res in db.Employeetrainingnews
                            on t.Courseid equals res.Courseid
                            orderby t.Enddate descending
                            select new { t.Enddate }).FirstOrDefault();

            if (res1 == null)
            {
                //not in any training
                return true;
            }
            else
            {
                    //Already in another training
                    return false;
                    throw new Exception("Already in another Training");

                    if (res1.Enddate >= DateTime.Now)
                    {
                        return false;
                    }
              
            }
            return true;
        }

        public List<Bussiness_Entities.Attendance> EmployeeAttendance(DateTime Date)
        {

            var res = from t in db.Attendances

                      where t.Date == Date.Date
                      select t;
            //var res1 = from t in db.Coursenames
            // select t;
            List<Bussiness_Entities.Attendance> display = new List<Bussiness_Entities.Attendance>();
            foreach (DAL.Attendance item in res)
            {
                display.Add(new Bussiness_Entities.Attendance { Employeeid = item.Employeeid, Date = item.Date, Courseid = item.Courseid, Status = item.Status });
            }
            return display;
        }

        public string getHRid(string hrname)
        {
            var res = (from t in db.HRs
                       orderby t.HRname descending
                       where t.HRname == hrname
                       select t).FirstOrDefault();
            return res.HRid;
        }
        public string getManagerid(string name)
        {
            var res = (from t in db.Managers
                       orderby t.Managername descending
                       where t.Managername == name
                       select t).FirstOrDefault();
            return res.Managerid;
        }
        public string getEmployeeid(string name)
        {
            var res = (from t in db.Employees
                       orderby t.Employeeid descending
                       where t.EmployeeName == name
                       select t).FirstOrDefault();
            return res.Employeeid;
        }
        public bool courseisstartedorended(string courseid)
        {
            int x = Int32.Parse(courseid);
            var res = (from t in db.Trainings
                       where t.Courseid == x
                       select t).FirstOrDefault();
            if (res.Startdate.Date >= DateTime.Now.Date && res.Enddate <= DateTime.Now.Date)
            {
                return false;
            }
            return true;

        }

        public int removecourse(int coursenameid, int courseid)
        {
            var res1 = (from t in db.Employeetrainingnews
                        where t.Courseid == courseid
                        select t);
            foreach (var item in res1)
            {
                db.Employeetrainingnews.Remove(item);
            }
            db.SaveChanges();

            var res = (from t in db.Trainings
                       where t.Coursenameid == coursenameid && t.Courseid == courseid
                       select t).FirstOrDefault();
            db.Trainings.Remove(res);
            return db.SaveChanges();
        }

        public string getmanagername(string managerid)
        {


            var res = (from t in db.Managers
                       where t.Managerid == managerid
                       select t).FirstOrDefault();
            return res.Managername;

        }

        public List<Bussiness_Entities.Employeetrainingnew> displayregisteredtrainings()
        {
            var res = from t in db.Employeetrainingnews
                      select t;

            List<Bussiness_Entities.Employeetrainingnew> display = new List<Bussiness_Entities.Employeetrainingnew>();

            foreach (DAL.Employeetrainingnew item in res)
            {
                display.Add(new Bussiness_Entities.Employeetrainingnew
                {
                    Employeeid = item.Employeeid,
                    Courseid = item.Courseid,
                    Status = item.Status
                });
            }
            return display;
        }


        public int HRLogin(string userid, string password)
        {
            byte[] p = Encoding.ASCII.GetBytes(password);

            var res = (from t in db.HRs
                       where t.HRid == userid & t.Password == p
                       select t);

            if (res.Count() > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public bool approvedbymanager(string employeeid)
        {
            var res = (from t in db.Employeetrainingnews
                       where t.Employeeid == employeeid
                       select t).FirstOrDefault();
            if (res.Status == "Approved")
            {
                return true;
            }
            return false;
        }

        public int Register(string employeeid, string courseid)
        {
            try
            {
                emptrain.Courseid = Int32.Parse(courseid);
                emptrain.Employeeid = employeeid;
                emptrain.Status = "Pending";
                db.Employeetrainingnews.Add(emptrain);
                return db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Request was also sent earlier , check status table");
            }

        }

        public int EditDetails(string employeeid, string emailid, string phonenumber, string address)
        {
            var res = (from t in db.Employees
                       where t.Employeeid == employeeid
                       select t).FirstOrDefault();

            {
                if (emailid == "")
                {
                    emailid = res.Emailid;
                }
                if (phonenumber == "")
                {
                    phonenumber = res.Phonenumber;
                }
                if (address == "")
                {
                    address = res.Address;
                }
            }

            //foreach (Employee item in res)
            //{
            //    item.Emailid = emailid;
            //    item.Phonenumber = phonenumber;
            //    item.Address = address;
            //}
            String s = string.Format($"Update Employee set Emailid='{emailid}',Phonenumber='{phonenumber}',[Address]='{address}' where Employeeid='{employeeid}' ");
            return db.Database.ExecuteSqlCommand(s);
        }

        public int canceltraining(string empid, int courseid)
        {
            var res = (from t in db.Employeetrainingnews
                       where t.Employeeid == empid && t.Courseid == courseid
                       select t).FirstOrDefault();

            //removeattendence(empid, courseid);

            db.Employeetrainingnews.Remove(res);

            return db.SaveChanges();
        }
        public void Approve(string Status, string employeeid, string reason)
        {
            var res = (from t in db.Employeetrainingnews
                       where t.Employeeid == employeeid
                       select t).First();
            res.Status = "Approved";

            db.SaveChanges();
        }
        public void Reject(string Status, string employeeid)
        {
            var res = (from t in db.Employeetrainingnews
                       where t.Employeeid == employeeid
                       select t).First();
            res.Status = "Rejected";
            db.SaveChanges();
        }

        public void removeattendence(string empid, int courseid)
        {
            var res1 = (from t in db.Attendances
                        where t.Employeeid == empid && t.Courseid == courseid
                        select t).First();
            db.Attendances.Remove(res1);
            db.SaveChanges();
        }
        public List<Bussiness_Entities.Employeetrainingnew> statusoftraining(string empid)
        {
            var res = from t in db.Employeetrainingnews
                      where t.Employeeid == empid
                      select t;
            List<Bussiness_Entities.Employeetrainingnew> display = new List<Bussiness_Entities.Employeetrainingnew>();
            foreach (DAL.Employeetrainingnew item in res)
            {
                display.Add(new Bussiness_Entities.Employeetrainingnew { Courseid = item.Courseid, Status = item.Status, Employeeid = item.Employeeid });
            }
            return display;
        }

        public object EmployeeLogin(string userid, string password)
        {
            byte[] p = Encoding.ASCII.GetBytes(password);

            var res = (from t in db.Employees
                       where t.Employeeid == userid & t.Password == p
                       select t);

            if (res.Count() > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public List<Bussiness_Entities.Training> displayongoingtraining()
        {
            var res = from t in db.Trainings
                      select t;


            List<Bussiness_Entities.Training> display = new List<Bussiness_Entities.Training>();

            foreach (DAL.Training item in res)
            {
                display.Add(new Bussiness_Entities.Training { Startdate = item.Startdate, Enddate = item.Enddate, Description = item.Description, Coursenameid = item.Coursenameid, Courseid = item.Courseid });
            }

            return display;
        }
        public List<Bussiness_Entities.Coursename> displaycoursename()
        {
            var res = from t in db.Coursenames
                      select t;
            List<Bussiness_Entities.Coursename> display = new List<Bussiness_Entities.Coursename>();

            foreach (DAL.Coursename item in res)
            {
                display.Add(new Bussiness_Entities.Coursename { Coursenameid = item.Coursenameid, Coursename1 = item.Coursename1 });
            }

            return display;
        }

        public object ManagerLogin(string userid, string password)
        {
            byte[] p = Encoding.ASCII.GetBytes(password);

            var res = (from t in db.Managers
                       where t.Managerid == userid & t.Password == p
                       select t);

            if (res.Count() > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public bool employeeintraining(string employeeid)
        {
            var res = from t in db.Employeetrainingnews
                      where t.Employeeid == employeeid
                      select t;
            if (res.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public int Giveattendance(string employeeid, int courseid)
        {
            a.Courseid = courseid;
            a.Date = DateTime.Now;
            a.Employeeid = employeeid;
            a.Status = "Present";
            db.Attendances.Add(a);
            return db.SaveChanges();
        }
        public List<Bussiness_Entities.Feedback> Displayfeedback(string dropdown, string value)
        {

            List<Bussiness_Entities.Feedback> display = new List<Bussiness_Entities.Feedback>();

            if (dropdown == "Course ID")
            {
                int c = Int16.Parse(value);

                var res = from t in db.Feedbacks
                          where t.Courseid == c
                          select t;
                foreach (var item in res)
                {
                    display.Add(new Bussiness_Entities.Feedback
                    {
                        Employeeid = item.Employeeid,
                        Courseid = item.Courseid,
                        Description = item.Description,
                        Question1 = item.Question1,
                        Question2 = item.Question2,
                        Question3 = item.Question3,
                        Question4 = item.Question4,
                        Question5 = item.Question5
                    });
                }
            }
            else if (dropdown == "Employee ID")
            {
                var res = from t in db.Feedbacks
                          where t.Employeeid == value
                          select t;
                foreach (var item in res)
                {
                    display.Add(new Bussiness_Entities.Feedback
                    {
                        Employeeid = item.Employeeid,
                        Courseid = item.Courseid,
                        Description = item.Description,
                        Question1 = item.Question1,
                        Question2 = item.Question2,
                        Question3 = item.Question3,
                        Question4 = item.Question4,
                        Question5 = item.Question5
                    });
                }
            }

            return display;
        }
        public int HRRegistration(string hrname, string password, string mailid)
        {
            String s = string.Format($"Insert into HR (HRname,Password,Emailid)values('{hrname}',CONVERT(varbinary,'{password}'),'{mailid}')");
            return db.Database.ExecuteSqlCommand(s);
        }
        public int ManagerRegistration(string managername, string password, string mailid, string deptname)
        {

            var res1 = (from t in db.Departments
                        where t.Departmentname == deptname
                        select t);

            if (res1.Count() == 0)
            {
                String s1 = string.Format($"insert into Department (Departmentname)values('{deptname}')");
                db.Database.ExecuteSqlCommand(s1);

                var res = (from t in db.Departments
                           where t.Departmentname == deptname
                           select t).First();
                int i = res.Departmentid;

                String s = string.Format($"Insert into Manager (Managername,Password,Emailid,Departmentid)values('{managername}',CONVERT(varbinary,'{password}'),'{mailid}',{i})");
                return db.Database.ExecuteSqlCommand(s);

            }

            else if (res1.Count() > 0)
            {
                var res = (from t in db.Departments
                           where t.Departmentname == deptname
                           select t).First();
                int i = res.Departmentid;

                String s = string.Format($"Insert into Manager (Managername,Password,Emailid,Departmentid)values('{managername}',CONVERT(varbinary,'{password}'),'{mailid}',{i})");
                return db.Database.ExecuteSqlCommand(s);
            }

            return 0;
        }

        public int EmployeeRegistration(string username, string password, string manid, string HRid, string address, string mailid, string phonenumber)
        {
            //var res1 = (from t in db.Managers
            //            where t.Departmentid == deptid
            //            select t).First();
            //string m = res1.Managerid;

            String s2 = string.Format($"Insert into Employee (EmployeeName,Password,Managerid,HRid,Address,Emailid,Phonenumber)values('{username}',CONVERT(varbinary,'{password}'),'{manid}','{HRid}','{address}','{mailid}','{phonenumber}')");
            return db.Database.ExecuteSqlCommand(s2);
        }

    }
}

