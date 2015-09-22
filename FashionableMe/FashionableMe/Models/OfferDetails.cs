using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FashionableMe.Models
{
    public class OfferDetails
    {
        public int OfferID { get; set; }
        [Required]
        public string OfferName { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string OfferDescription { get; set; }
        [Required]
        public int OfferApparelID { get; set; }
        [Required]
        public decimal OfferDiscount { get; set; }

        public AddApparel apparel { get; set; }



    }


}