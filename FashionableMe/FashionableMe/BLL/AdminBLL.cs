using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FashionableMe.Models;
using FashionableMe.DataAccessLayer;
using FashionableMe.Utils;
using System.Web.Mvc;
using System.IO;
using FashionableMe.Controllers;

namespace FashionableMe.BLL
{
    public class AdminBLL
    {
        public List<Apparel> fetchProductByCategory(string category)
        {
            string cat = UtilityFunctions.parseInputCategoryToDBFormat(category);
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
            string dateFormatted = UtilityFunctions.parseInputDateToDBFormat(dateToProcess);
            return obj.getOfferDetails(dateFormatted); 
        }

        public bool UpdateOffer(string offerDate, string offerName, string offerDescription, string offerDiscount)
        {
            AdminDal obj = new AdminDal();
            return obj.updateOfferByDate(UtilityFunctions.parseInputDateToDBFormat(offerDate), offerName, offerDescription, offerDiscount);

        }

        public bool DeleteOffer(string offerDate)
        {
            AdminDal obj = new AdminDal();
            return obj.deleteOfferByDate(UtilityFunctions.parseInputDateToDBFormat(offerDate));

        }


        public bool addApparel(AddApparel model, out string AppID)
        {
            AdminDal obj = new AdminDal();
            model.apparel.ApparelCategory = UtilityFunctions.parseInputCategoryToDBFormat(model.apparel.ApparelCategory);
            model.apparel.ApparelImage = "/"+AdminController.ImagePath;
            string fullPath = HttpContext.Current.Server.MapPath("~/") + AdminController.ImagePath;
            if (model.apparel.ApparelImage == null)
                model.apparel.ApparelImage = string.Empty;
            if (obj.addProduct(model, out AppID))
            {
                System.IO.File.Move( fullPath+"TempImage.jpg", fullPath + AppID + ".jpg");
                return true;
            }
            
            return false;
        }

        public List<DropDownFormat> getBrandNames()
        {
            AdminDal obj = new AdminDal();
            List<string> result = new List<string>();
            List<DropDownFormat> res = new List<DropDownFormat>();
            result = obj.getBrandNames();
            foreach (var item in result)
	        {
                res.Add(new DropDownFormat(){ name=item, value=item });
	        }
            return res;
        }
        
        public List<DropDownFormat> getApparelNameByBrand(string brand)
        {
            AdminDal obj = new AdminDal();
            List<string> result = new List<string>();
            List<DropDownFormat> res = new List<DropDownFormat>();
            result = obj.getApparelNameByBrand(brand.Trim());
            foreach (var item in result)
	        {
                res.Add(new DropDownFormat(){ name=item, value=item });
	        }
            return res;
        }

        public List<AddApparel> getApparelsByBrandAndName(string name, string brand)
        {
            AdminDal obj = new AdminDal();
            List<AddApparel> result = new List<AddApparel>();
            result = obj.getApparelsByBrandAndName(name, brand);
            return result;
        }

        public bool UpdateApparel(string apparelID, string cost, string discount, string quantity, string category, string size)
        {
            AdminDal obj = new AdminDal();
            return obj.UpdateApparel(Convert.ToInt32(apparelID), Convert.ToDecimal(cost), Convert.ToDecimal(discount), Convert.ToInt32(quantity), category, size);
        }
    }
}