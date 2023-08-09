using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Models
{
    public class Employee
    {
        [Key]
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime EmployeeDOB { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Hobbies { get; set; }
    }
}
