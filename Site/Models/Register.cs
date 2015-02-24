using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Site.Models
{
    public class Register
    {
        public int iID { get; set; }


        [Required(ErrorMessage = "Please Enter First Name")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Length must be 2 to 15 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please Enter Valid First Name")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Please Enter Last Name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Length must be 2 to 20 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please Enter Valid Last Name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Please Enter User Name")]
        [Remote("CheckUserName", "Account")]
        public string UserID { get; set; }


        [Required(ErrorMessage = "Please Enter Email ID")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please Enter Valid Email ID ")]
        [Remote("CheckEmailExist", "Account")]

        public string EmailID { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password should not be less than 8 characters")]
        [RegularExpression(@"^.*(?=.{10,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Please Enter Valid Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password Enter Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile Number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Mobile no. should be  10 characters")]
        [RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "Please Enter Valid Mobile Number")]
        [Remote("CheckMobileExist", "Account")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }
    }
    public class Login
    {
        [Required(ErrorMessage = "Please Enter Email ID")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please Enter Valid Email ID ")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
    public class ForgotPassword
    {
        [Required(ErrorMessage = "Please Enter Email ID")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please Enter Valid Email ID ")]
        public string EmailID { get; set; }
    }
    public class ChangePassword
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^.*(?=.{10,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Please Enter Valid Password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^.*(?=.{10,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Please Enter Valid Password")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


    }
}
