using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FashionableMe.Models
{
    public class DetailsViewModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "Atmost 30 characters allowed.")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Atmost 50 characters allowed.")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Atmost 25 characters allowed.")]
        public string City { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Atmost 25 characters allowed.")]
        public string State { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]*)$", ErrorMessage = "Only digits are allowed")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Only 6 characters allowed.")]
        public string Pincode { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0-9]*)$", ErrorMessage = "Only digits are allowed")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Only 10 characters allowed.")]
        public string MobileNumber { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public bool IsMale { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Minimum 8 and atmost 15 characters allowed.")]
        [RegularExpression(@"([0-9]+[a-zA-Z]|[a-zA-Z]+[0-9])[A-Za-z0-9]*", ErrorMessage = "Atleast one alphabet and one digit required.")]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Minimum 8 and atmost 15 characters allowed.")]
        [RegularExpression(@"([0-9]+[a-zA-Z]|[a-zA-Z]+[0-9])[A-Za-z0-9]*", ErrorMessage = "Atleast one alphabet and one digit required.")]
        [Display(Name = "New Password")]
        public string Password { get; set; }


        //[Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

    }
}