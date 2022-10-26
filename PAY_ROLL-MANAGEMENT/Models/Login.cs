using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PAY_ROLL_MANAGEMENT.Models
{
    public class Login
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Please enter valid username")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}