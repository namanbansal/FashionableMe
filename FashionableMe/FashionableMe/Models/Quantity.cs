using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FashionableMe.Models
{
    public class Quantity
    {
        public int ApparelID { get; set; }
        public decimal ApparelCost { get; set; }
        public string ApparelSize { get; set; }
        public decimal ApparelDiscount { get; set; }
        [RegularExpression("[1-9][0-9]*")]
        public int ApparelQuantity { get; set; }

    }
}