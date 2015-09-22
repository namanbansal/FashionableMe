using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FashionableMe.Models
{
    public class AddApparel
    {
        public Apparel apparel { get; set; }
        [RegularExpression("^[1-9]+[0-9]*$", ErrorMessage = "Quantity should be only integer greater than zero")]
        public int small { get; set; }
        [RegularExpression("^[1-9]+[0-9]*$", ErrorMessage = "Quantity should be only integer greater than zero")]
        public int medium { get; set; }
        [RegularExpression("^[1-9]+[0-9]*$", ErrorMessage = "Quantity should be only integer greater than zero")]
        public int large { get; set; }

        public decimal costForSmall { get; set; }
        public decimal costForMedium { get; set; }
        public decimal costForLarge { get; set; }

        public AddApparel()
        {
            apparel = new Apparel();
        }
    }
}