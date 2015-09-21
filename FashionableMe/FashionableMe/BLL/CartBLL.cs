using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FashionableMe.DataAccessLayer;
using FashionableMe.Models;

namespace FashionableMe.BLL
{
    public class CartBLL
    {
        private CartDAL dalObj;
        public CartBLL()
        {
            dalObj = new CartDAL();
        }
        public Apparel getApparelForCart(string apparelID, string size)
        {
            return dalObj.getApparelForCart(apparelID, size);
        }
    }
}