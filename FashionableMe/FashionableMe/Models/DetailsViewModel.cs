using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

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
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Only 6 characters allowed.")]
        public string Pincode { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Only 10 characters allowed.")]
        public string MobileNumber { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public bool IsMale { get; set; }

    }
}