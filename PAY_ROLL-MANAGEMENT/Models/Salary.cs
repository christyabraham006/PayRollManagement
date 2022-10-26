using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PAY_ROLL_MANAGEMENT.Models
{
    public class Salary
    {

        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string BasicSalary { get; set; }
        public string Allowances { get; set; }
        public string Deduction { get; set; }
        public string NetSalary { get; set; }
        public string ApprovedBy { get; set; }
    }
}