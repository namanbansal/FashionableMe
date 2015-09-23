using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FashionableMe.Models
{
    public class CartItem
    {
        private Apparel apparel = new Apparel();
        [RegularExpression("^[1-9]+[0-9]*$", ErrorMessage = "Quantity should be only integer greater than zero")]
        private int quantity;

        public Apparel Apparel
        {
            get { return apparel; }
            set { apparel = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public string OfferID { get; set; }

        public CartItem() { }

        public CartItem(Apparel apparel, int quantity, string offerID)
        {
            this.apparel = apparel;
            this.quantity = quantity;
            this.OfferID = offerID;
        }
    }

    public class ItemDetails
    {
        public string id { get; set; }
        public string name { get; set; }
        public string brand { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public int newRating { get; set; }
        public string newComment { get; set; }
    }
}