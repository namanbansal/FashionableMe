using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FashionableMe.Models
{
    public class CreditCard
    {
        [Required]
        [RegularExpression(@"^([a-z A-Z]*)$", ErrorMessage = "Only letters and spaces are allowed")]
        [StringLength(30, ErrorMessage = "Atmost 30 characters allowed.")]
        public string CardHolderName { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]*)$", ErrorMessage = "Only digits are allowed")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Only 4 digits allowed.")]
        public int CardNumber1 { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]*)$", ErrorMessage = "Only digits are allowed")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Only 4 digits allowed.")]
        public int CardNumber2 { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]*)$", ErrorMessage = "Only digits are allowed")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Only 4 digits allowed.")]
        public int CardNumber3 { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]*)$", ErrorMessage = "Only digits are allowed")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Only 4 digits allowed.")]
        public int CardNumber { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]*)$", ErrorMessage = "Only digits are allowed")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Only 3 digits allowed.")]
        [DataType(DataType.Password)]
        public int CVV { get; set; }

       



    }
}