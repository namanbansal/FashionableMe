using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FashionableMe.Models;
using FashionableMe.DataAccessLayer;

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
    }
}