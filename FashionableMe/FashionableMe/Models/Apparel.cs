﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionableMe.Models
{
    public class Apparel
    {
        public string ApparelID { get; set; }
        public string ApparelName { get; set; }
        public string BrandName { get; set; }
        public double ApparelCost { get; set; }
        public string Description { get; set; }
        public string ApparelImage { get; set; }
        public string ApparelCategory { get; set; }
        public DateTime ApparelAddTime { get; set; }
        public int Rating { get; set; }
    }
}