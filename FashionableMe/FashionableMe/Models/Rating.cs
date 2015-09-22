using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionableMe.Models
{
    public class Rating
    {
        public string ApparelID { get; set; }
        public string UserID { get; set; }
        public int ApparelRating { get; set; }
        public string Comment { get; set; }
        public DateTime AddTime { get; set; }
        public int userRating { get; set; }
        public string userComment { get; set; }
        public bool canRate { get; set; }

    }
}