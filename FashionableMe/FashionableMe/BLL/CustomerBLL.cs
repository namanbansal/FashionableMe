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
        public List<Apparel> GetProductByCategory(string category)
        {
            return dalObj.GetProductByCategory(category);
        }
        public List<Quantity> GetQuantityDetails(string apparelID)
        {
            return dalObj.GetQuantityDetails(Convert.ToInt32(apparelID.Trim()));
        }

        public List<Offer> GetTodaysOffer()
        {
            List<Offer> offer = new List<Offer>();
            offer.Add(dalObj.GetTodaysOffer(Convert.ToString(DateTime.Now.Date.ToString("yyyy/MM/dd"))));

            return offer;
        }

        public List<Apparel> GetApparelByID(int id)
        {
            return dalObj.GetApparelByID(id);
        }

        public List<MyOrder> GetOrderDetails(string UserID)
        {
            CartDAL cartDalObj = new CartDAL();
            return cartDalObj.GetOrderDetails(UserID.Trim());
        }
    }
}