using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FashionableMe.Models
{
    public class Offer
    {
        public string OfferID { get; set; }
        [Required]
        public string OfferName { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string OfferDescription { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime OfferDate { get; set; }
        [Required]
        public int ApparelID { get; set; }
        [Required]
        [RegularExpression(@"^[1-9](?:\d*\.)?\d*$", ErrorMessage="Discount should be only numeric greater than zero")]
        public decimal Discount { get; set; }

        
    }

   
}