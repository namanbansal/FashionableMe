using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionableMe.Utils
{
    public class UtilityFunctions
    {
        public List<SelectListItem> getCategoryDropDown()
        {
            List<SelectListItem> categoryTypes = new List<SelectListItem>();
            categoryTypes.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
            categoryTypes.Add(new SelectListItem { Text = "Male", Value = "1" });
            categoryTypes.Add(new SelectListItem { Text = "Female", Value = "2" });
            categoryTypes.Add(new SelectListItem { Text = "Kids", Value = "3" });

            return categoryTypes;
            
        }
    }
}