using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionableMe.Models
{
    public class Quantity
    {
        public int ApparelID { get; set; }
        public decimal ApparelCost { get; set; }
        public string ApparelSize { get; set; }
        public decimal ApparelDiscount { get; set; }
        public int ApparelQuantity { get; set; }

    }
}