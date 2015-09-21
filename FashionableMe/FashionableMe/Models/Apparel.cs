using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FashionableMe.Models
{
    public class Apparel
    {
        public int ApparelID { get; set; }
        [Required]
        public string ApparelName { get; set; }
        [Required]
        public string BrandName { get; set; }
        [Required]
        public decimal ApparelCost { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ApparelImage { get; set; }
        [Required]
        public string ApparelCategory { get; set; }
        public DateTime ApparelAddTime { get; set; }
        public int ApparelRating { get; set; }
        public string ApparelSize { get; set; }
        public int ApparelQuantity{get; set;}
        [Required]
        [RegularExpression(@"^[1-9](?:\d*\.)?\d+$", ErrorMessage = "Discount should be only numeric greater than zero")]
        public decimal ApparelDiscount { get; set; }

    }
}