using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FashionableMe.Models;
using FashionableMe.DataAccessLayer;

namespace FashionableMe.BLL
{
    public class AdminBLL
    {
        public List<Apparel> fetchProductByCategory(int category)
        {
            string cat="";
            AdminDal obj = new AdminDal();
            if (category == 1)
                cat = "Male";
            else if (category == 2)
                cat = "Female";
            else if (category == 3)
                cat = "Kids";
            List<Apparel> result = new List<Apparel>();
            result = obj.getProductByCategory(cat);
            return result;
        }

        public bool addOffer(Offer model)
        {
            AdminDal obj = new AdminDal();
            if (obj.AddOffer(model))
                return true;
            return false;

        }

        public List<Offer> fetchOfferByDate(string dateToProcess)
        {
            
            AdminDal obj = new AdminDal();
            string date = dateToProcess.Substring(8, 2);
            string month = dateToProcess.Substring(5, 2);
            string year = dateToProcess.Substring(0, 4);
            //string Month="";
            //bool flag = true;
            //for (int i = 0; i < month.Length; i++)
            //{
            //    if (month[i] == '0' && flag)
            //        flag = false;
            //    else
            //        Month += month[i];

            //}
            string dateFormatted = month + "/" + date + "/" + year;
            List<Offer> result = new List<Offer>();
            //result = obj.getOfferDetails(dateFormatted);
            return result;
        }
        
    }
}