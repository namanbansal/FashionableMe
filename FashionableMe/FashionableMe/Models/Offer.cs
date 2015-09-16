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
        
        public string OfferName { get; set; }
        [DataType(DataType.MultilineText)]
        public string OfferDescription { get; set; }
        public string Category { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime OfferDate { get; set; }
        public int ApparelID { get; set; }
        public decimal Discount { get; set; }

    }

   
}