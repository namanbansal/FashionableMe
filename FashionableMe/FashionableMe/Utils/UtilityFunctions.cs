using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionableMe.Utils
{
    public static class UtilityFunctions
    {
        public static List<SelectListItem> getCategoryDropDown()
        {
            List<SelectListItem> categoryTypes = new List<SelectListItem>();
            categoryTypes.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
            categoryTypes.Add(new SelectListItem { Text = "Male", Value = "1" });
            categoryTypes.Add(new SelectListItem { Text = "Female", Value = "2" });
            categoryTypes.Add(new SelectListItem { Text = "Kids", Value = "3" });

            return categoryTypes;
            
        }

        public static string parseInputDateToDBFormat(string dateToProcess)
        {
            string date = dateToProcess.Substring(8, 2);
            string month = dateToProcess.Substring(5, 2);
            string year = dateToProcess.Substring(0, 4);
            string dateFormatted = month + "/" + date + "/" + year;

            return dateFormatted;
        }

        public static string parseInputCategoryToDBFormat(string val)
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
    }
}