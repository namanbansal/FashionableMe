using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FashionableMe.Models;
using System.Web.Mvc;

namespace FashionableMe.BLL
{
    public class AccountBLL
    {
        public bool sendRegisterDetails(Customer cust)
        {
            return true;
        }

        public bool checkLoginDetails(LoginModel loginDetails)
        {
            return true;
        }

        public DetailsViewModel getCustomerDetails(string userID)
        {
            DetailsViewModel result = new DetailsViewModel();
            return result;
        }
    }
}