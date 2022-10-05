using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bussiness_Entities
{

    public partial class Employeetrainingnew
    {
        public string Employeeid { get; set; }
        public int Courseid { get; set; }
        public string Status { get; set; }

      
    }
    public class Attendance
        {
            public string Employeeid { get; set; }
            public DateTime Date { get; set; }
            public int Courseid { get; set; }
            public string Status { get; set; }
        }

        public class Coursename
        {
            public int Coursenameid { get; set; }
            public string Coursename1 { get; set; }
        }

        public class Department
        {
            public int Departmentid { get; set; }
            public string Departmentname { get; set; }
        }

        public class Employee
        {
            public int id { get; set; }
            public string Employeeid { get; set; }
            public byte[] Password { get; set; }
            public string EmployeeName { get; set; }
            public string Managerid { get; set; }
            public string HRid { get; set; }
            public string Address { get; set; }
            public string Emailid { get; set; }
            public string Phonenumber { get; set; }
        }

        public class Employeetraining
        {
            public string Employeeid { get; set; }
            public int Courseid { get; set; }
            public string Status { get; set; }
        }

        public class Feedback
        {
            public int feedbackid { get; set; }
            public string Employeeid { get; set; }
            public string Description { get; set; }
            public int Courseid { get; set; }

        public Nullable<int> Question1 { get; set; }
        public Nullable<int> Question2 { get; set; }
        public Nullable<int> Question3 { get; set; }
        public Nullable<int> Question4 { get; set; }
        public Nullable<int> Question5 { get; set; }
    }

        public class HR
        {
            public int id { get; set; }
            public string HRid { get; set; }
            public byte[] Password { get; set; }
            public string HRname { get; set; }
            public string Emailid { get; set; }
        }

        public class Manager
        {
            public int id { get; set; }
            public string Managerid { get; set; }
            public byte[] Password { get; set; }
            public string Managername { get; set; }
            public string Emailid { get; set; }
            public int Departmentid { get; set; }
        }

        public class Training
        {
            public int Courseid { get; set; }
            public int Coursenameid { get; set; }
            public DateTime Startdate { get; set; }
            public DateTime Enddate { get; set; }
            public string Description { get; set; }
        }

    public class displaytraining
    {
        public int Courseid { get; set; }
        public string Coursename { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public string Description { get; set; }
    }

}
