using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xunit;
using Xunit.Sdk;
using System.ComponentModel.DataAnnotations;

namespace PAY_ROLL_MANAGEMENT.Models
{
    public class EmployeeLeave
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        [Required(ErrorMessage = "Please enter name"), MaxLength(30)]
        [RegularExpression(@"[A-Za-z ]{1,32}", ErrorMessage = "Not a Valid Name.")]
        public string FullName { get; set; }
        public string RequestId { get; set; }
        [Required(ErrorMessage = "Specify the reason for leave"), MaxLength(100)]
        public string ReasonforLeave { get; set; }
        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Please enter Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [Required(ErrorMessage = "Please enter End date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }
        [Display(Name = "End Date")]

        public int NoofDays { get; set; }


    }
}