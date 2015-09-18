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
        public List<Apparel> fetchProductByCategory(string category)
        {
            string cat = parseInputCategoryToDBFormat(category);
            AdminDal obj = new AdminDal();
            
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

        public Offer fetchOfferByDate(string dateToProcess)
        {
            AdminDal obj = new AdminDal();
            string dateFormatted = parseInputDateToDBFormat(dateToProcess);
            return obj.getOfferDetails(dateFormatted); 
        }

        public bool UpdateOffer(string offerDate, string offerName, string offerDescription, string offerDiscount)
        {
            AdminDal obj = new AdminDal();
            return obj.updateOfferByDate(parseInputDateToDBFormat(offerDate), offerName, offerDescription, offerDiscount);

        }

        public bool DeleteOffer(string offerDate)
        {
            AdminDal obj = new AdminDal();
            return obj.deleteOfferByDate(parseInputDateToDBFormat(offerDate));

        }

        public string parseInputDateToDBFormat(string dateToProcess)
        {
            string date = dateToProcess.Substring(8, 2);
            string month = dateToProcess.Substring(5, 2);
            string year = dateToProcess.Substring(0, 4);
            string dateFormatted = month + "/" + date + "/" + year;

            return dateFormatted;
        }

        public string parseInputCategoryToDBFormat(string val)
        {
            string cat = "";
            int category = Convert.ToInt32(val.Trim());
            if (category == 1)
                cat = "Male";
            else if (category == 2)
                cat = "Female";
            else if (category == 3)
                cat = "Kids";

            return cat;
        }
        public bool addApparel(AddApparel model)
        {
            AdminDal obj = new AdminDal();
            model.apparel.ApparelCategory = parseInputCategoryToDBFormat(model.apparel.ApparelCategory);
            if (model.apparel.ApparelImage == null)
                model.apparel.ApparelImage = string.Empty;
            if (obj.addProduct(model))
                return true;
            return false;
        }
        
    }
}