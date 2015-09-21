using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionableMe.BLL;
using FashionableMe.Models;
using FashionableMe.Utils;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;


namespace FashionableMe.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        UtilityFunctions util = new UtilityFunctions();

        public ActionResult Index()
        {
            Session["s"] = "b";
            
            return View();
        }

        //
        // GET: /Admin/Details/5

        public ActionResult Offer()
        {
            ViewBag.categoryData = util.getCategoryDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult Offer(Offer model)
        {
            AdminBLL obj = new AdminBLL();
           List<SelectListItem> defaultList = new List<SelectListItem>();
           defaultList = util.getCategoryDropDown();
           // defaultList[0].Selected= false;
            
           // defaultList[Convert.ToInt32(ModelState["temp"])].Selected = true;
            ViewBag.categoryData = defaultList;

            if (obj.addOffer(model))
                ViewBag.Message = "Offer Added Successfully";
            else
            {
                if (Convert.ToString(Session["ErrorMessage"]) == "Offer with Same Date exists")
                {
                    ViewBag.Message = "Offer with Same Date exists.Try Updating with same date!";
                    
                }
                else
                    ViewBag.Message = "Unable to add Offer";
            }
            return View(model); ;
        }

        [HttpPost]
        public ActionResult searchByCategory(string val)
        {
            AdminBLL obj = new AdminBLL();
            List<Apparel> listApparel = obj.fetchProductByCategory(val);
            //ViewBag.Message = listApparel[0].ApparelName;
            string msg = "0";
            if (listApparel.Count > 0)
                msg = listApparel[0].ApparelName;
            return PartialView("_apparelDetailsTable", listApparel);
        }

        [HttpPost]
        public string fetchByDate(string date)
        {
            AdminBLL obj = new AdminBLL();
            Offer model = obj.fetchOfferByDate(date);
            JavaScriptSerializer jsonobj = new JavaScriptSerializer();
            
            return (jsonobj.Serialize(model));
        }


        public ActionResult Apparel()
        {
            ViewBag.categoryData = util.getCategoryDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult Apparel(AddApparel model)
        {
            ViewBag.categoryData = util.getCategoryDropDown();
            AdminBLL obj = new AdminBLL();
            bool result = obj.addApparel(model);
            if (result)
                ViewBag.Message = "Sucessfully added apparel";
            else
                ViewBag.Message = "Unable to add apparel";

            return View(model);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public string UpdateOffer(string offerDate, string offerName, string offerDescription, string offerDiscount)
        {
            AdminBLL obj = new AdminBLL();
            if (obj.UpdateOffer(offerDate, offerName, offerDescription, offerDiscount))
            {
                return "true";
            }
            return "false";

        }

        [HttpPost]
        public string DeleteOffer(string offerDate)
        {
            AdminBLL obj = new AdminBLL();
            if (obj.DeleteOffer(offerDate))
            {
                return "true";
            }
            return "false";

        }

        //
        // GET: /Admin/Delete/5

        [HttpPost]
        public ActionResult getBrandNames()
        {
            AdminBLL obj = new AdminBLL();
            List<DropDownFormat> result = obj.getBrandNames();
            SelectList resList = new SelectList(result, "value", "name");
            return Json(resList);
            
        }
        

        [HttpPost]
        public ActionResult getApparelNameByBrand(string brand)
        {
            AdminBLL obj = new AdminBLL();
            List<DropDownFormat> result = obj.getApparelNameByBrand(brand);
            SelectList resList = new SelectList(result, "value", "name");
            return Json(resList);
            
        }


        [HttpPost]
        public ActionResult getApparelsByBrandAndName(string name, string brand)
        {
            AdminBLL obj = new AdminBLL();
            List<AddApparel> listApparel = obj.getApparelsByBrandAndName(name, brand);
            return PartialView("_updateApparel", listApparel);

        }

        [HttpPost]
        public bool UpdateApparel(string apparelID, string  cost, string discount, string quantity, string category, string size )
        {
            AdminBLL obj = new AdminBLL();
            bool result = obj.UpdateApparel(apparelID, cost, discount, quantity, category, size);
            return result;

        }
        //
        // POST: /Admin/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
