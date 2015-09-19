using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionableMe.Models
{
    public class CartItem
    {
        private Apparel apparel = new Apparel();
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

        public CartItem() { }

        public CartItem(Apparel apparel, int quantity)
        {
            this.apparel = apparel;
            this.quantity = quantity;
        }
    }
}