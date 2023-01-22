using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineTutorSystem.Models
{
    public class tutor
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Please Enter First Name")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Name cannot contain Mutiple words, number or spaces")]
        [Display(Name = "First name")]


        public string fname { get; set; }


        [Required(ErrorMessage = "Please Enter Last Name")]
        [Display(Name = "Last name")]
        [RegularExpression(@"^(([A-za-z]+[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Name cannot contain Mutiple words, number or spaces")]
        public string lname { get; set; }

        [Required(ErrorMessage = "Please Enter Gender")]
        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Required(ErrorMessage = "Please Enter Phone No")]
        [Display(Name = "Phone No")]
        [RegularExpression(@"^\(?([0]{1})\)?[-. ]?([3]{1})[-. ]?([0-9]{9})$", ErrorMessage = "Invalid Phone No, must start with '03' and contain 11 digits")]
        public String phone_no { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Please Enter Email")]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Select")]
        [Display (Name ="City")]
        public string city { get; set; }
        [Required(ErrorMessage = "Select")]
        [Display(Name = "Class")]
        public string Class { get; set; }


       


        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Password")]
        public string password { get; set; }


        [Required(ErrorMessage = "Please Enter confirm Password")]
        [Display(Name = "Confirm Password")]
        [Compare("password")]
        public string confirmpassword { get; set; }

        [Display(Name="Bio")]
        [MaxLength(300,ErrorMessage ="Bio limit is 300 characters, you've execeeded")]
        public string Bio { get; set; }


        [Required(ErrorMessage = "Please enter first name")]
        [Display(Name = "Profile Picture")]
        public byte[] img { get; set; }
    }
}