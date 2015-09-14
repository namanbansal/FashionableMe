using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FashionableMe.Models
{
    public class Customer
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
        [StringLength(6, MinimumLength=6, ErrorMessage = "Only 6 characters allowed.")]
        public string Pincode { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Only 10 characters allowed.")]
        public string MobileNumber { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name="Gender")]
        public bool IsMale { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Minimum 8 and atmost 15 characters allowed.")]
        [RegularExpression(@"^[a-zA-Z0-9_]*$", ErrorMessage = "Only alphabets, numbers and underscore allowed.")]
        public string UserID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Minimum 8 and atmost 15 characters allowed.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Minimum 8 and atmost 15 characters allowed.")]
        public string ConfirmPassword { get; set; }

    }
   
}