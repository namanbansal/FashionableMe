using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FashionableMe.Models;
using System.Web.Mvc;
using FashionableMe.DataAccessLayer;
using System.Security.Cryptography;
using System.Text;
namespace FashionableMe.BLL
{
    public class AccountBLL
    {
        public string hashPassword(string password)
        {
            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);

            // need MD5 to calculate the hash
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            // string representation (similar to UNIX format)
            string encoded = BitConverter.ToString(hash)
                // without dashes
               .Replace("-", string.Empty)
                // make lowercase
               .ToLower();
            return encoded;
        }
        public bool sendRegisterDetails(Customer cust)
        {
            CustomerDal dalobj = new CustomerDal();
            // step 1, calculate MD5 hash from input


            cust.Password = hashPassword(cust.Password);
            if(dalobj.AddCustomer(cust))
                return true;

            return false;
        }

        public bool checkLoginDetails(LoginModel loginDetails)
        {
            loginDetails.Password = hashPassword(loginDetails.Password);
            CustomerDal dalobj = new CustomerDal();
            if(dalobj.LoginCheck(loginDetails))
                return true;
            return false;
        }

        public DetailsViewModel getCustomerDetails(string userID)
        {
            DetailsViewModel result = new DetailsViewModel();
            return result;
        }
    }
}