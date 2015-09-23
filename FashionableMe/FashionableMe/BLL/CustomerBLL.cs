using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FashionableMe.Models;
using FashionableMe.DataAccessLayer;
using FashionableMe.Utils;

namespace FashionableMe.BLL
{
    public class CustomerBLL
    {
        private CustomerDal dalObj;

        public CustomerBLL()
        {
            dalObj = new CustomerDal();
        }
        public List<Apparel> getProductByCategory(string category)
        {
            return dalObj.getProductByCategory(category);
        }
        public List<Quantity> getQuantityDetails(string apparelID)
        {
            return dalObj.getQuantityDetails(Convert.ToInt32(apparelID.Trim()));
        }

        public List<Offer> getTodaysOffer()
        {
            List<Offer> offer = new List<Offer>();
            offer.Add(dalObj.getTodaysOffer(Convert.ToString(DateTime.Now.Date.ToString("yyyy/MM/dd"))));

            return offer;
        }

        public List<Apparel> getApparelByID(int id)
        {
            return dalObj.getApparelByID(id);
        }

        public List<MyOrder> GetOrderDetails(string UserID)
        {
            CartDAL cartDalObj = new CartDAL();
            return cartDalObj.GetOrderDetails(UserID.Trim());
        }
    }
}