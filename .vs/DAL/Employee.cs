//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Attendances = new HashSet<Attendance>();
            this.Feedbacks = new HashSet<Feedback>();
            this.Employeetrainingnews = new HashSet<Employeetrainingnew>();
        }
    
        public int id { get; set; }
        public string Employeeid { get; set; }
        
        public byte[] Password { get; set; }
        public string EmployeeName { get; set; }
        public string Managerid { get; set; }
        public string HRid { get; set; }
        public string Address { get; set; }
        public string Emailid { get; set; }
        public string Phonenumber { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual HR HR { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual Employeetraining Employeetraining { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employeetrainingnew> Employeetrainingnews { get; set; }
    }
}
