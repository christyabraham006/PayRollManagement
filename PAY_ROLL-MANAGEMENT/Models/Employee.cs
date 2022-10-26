using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pay_Roll_Management.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        [Required(ErrorMessage = "Please enter name"), MaxLength(30)]
        [RegularExpression(@"[A-Za-z ]{1,32}", ErrorMessage = "Not a Valid Name.")]
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please specify your Gender")]
        public string Gender { get; set; }
        [Display(Name = "Date of birth")]
        [Required(ErrorMessage = "Please enter Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateofBirth { get; set; }
        [Required(ErrorMessage = "Please enter the Contact number")]
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "Not a Valid Phone No.")]
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
        [Display(Name = "Designation")]
        [Required(ErrorMessage = "Please enter your Designation")]
        public string Designation { get; set; }
        [Display(Name = "Allocated Location")]
        [Required(ErrorMessage = "Please enter your Location")]
        public string AllocatedLocation { get; set; }
        [Display(Name = "Date of Joining")]
        [Required(ErrorMessage = "Please enter Date of Joining")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateofJoining { get; set; }
        [Display(Name = "Name of Manager")]
        [Required(ErrorMessage = "Please enter name of your manager")]
        public string NameofManager { get; set; }
        [Display(Name = "Home Address")]
        [Required(ErrorMessage = "Please enter your address")]
        public string HomeAddress { get; set; }
        public bool Admin { get; set; }
    }
}