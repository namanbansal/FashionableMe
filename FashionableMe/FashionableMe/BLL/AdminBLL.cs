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
            AdminDal obj = new AdminDal();
            if (category == "1")
                category = "Male";
            else if (category == "2")
                category = "Female";
            else if (category == "3")
                category = "Kids";

            return obj.getProductByCategory(category);
        }
    }
}