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

        public DetailsViewModel getShippingDetails(string customerID)
        {
            CustomerDal custDalObj = new CustomerDal();
            return custDalObj.getCustomerDetails(customerID);
        }

        public string InsertOrderDetails(List<MyOrder> orders)
        {
            return dalObj.InsertOrderDetails(orders);

        }
        public List<MyOrder> GetOrderDetails(string UserID)
        {
            return dalObj.GetOrderDetails(UserID);
        }
        
    }
}