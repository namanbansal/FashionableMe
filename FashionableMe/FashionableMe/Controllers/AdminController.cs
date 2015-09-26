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
using System.IO;

namespace FashionableMe.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public static string ImagePath = "Images/Apparels/"; //"App_Data/Images/";

        public bool checkAdmin()
        {
            if (Session["UserRole"] != null &&  Session["UserRole"].ToString() == "admin")
                return true;
            else 
                return false;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Offer");
        }

        //
        // GET: /Admin/Details/5
        
        public ActionResult Offer()
        {
            if(!checkAdmin())
                return (View("~/Views/Shared/Unauthorized.cshtml")); //

            ViewBag.categoryData = UtilityFunctions.getCategoryDropDown();
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Offer(Offer model)
        {
            AdminBLL obj = new AdminBLL();
           List<SelectListItem> defaultList = new List<SelectListItem>();
           defaultList = UtilityFunctions.getCategoryDropDown();
           // defaultList[0].Selected= false;
            
           // defaultList[Convert.ToInt32(ModelState["temp"])].Selected = true;
            ViewBag.categoryData = defaultList;

            if (obj.addOffer(model))
            {
                ViewBag.Message = "Offer Added Successfully";
                Session["OfferMessage"] = "false";


            }
            else
            {
                if (Convert.ToString(Session["ErrorMessage"]) == "Offer with Same Date exists")
                {
                    Session["OfferMessage"] = "true";
                    Session["ErrorMessage"] = "Offer with Same Date Exists. Try Updating the Offer!";
                    ViewBag.Message = "";

                }
                else
                {
                    Session["OfferMessage"] = "true";
                    Session["ErrorMessage"] = "Unable to add Offer";
                }
            }
            return View(model); ;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        public string fetchByDate(string date)
        {
            if (!checkAdmin())
                return ("<h1 style='Color:red'>You Are NOT AUTHORIZED to access this Page ..! <h1>");

            AdminBLL obj = new AdminBLL();
            Offer model = obj.fetchOfferByDate(date);
            JavaScriptSerializer jsonobj = new JavaScriptSerializer();
            
            return (jsonobj.Serialize(model));
        }


        public ActionResult Apparel()
        {
            if (!checkAdmin())
                return (View("~/Views/Shared/Unauthorized.cshtml"));

            ViewBag.categoryData = UtilityFunctions.getCategoryDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult Apparel(AddApparel model)
        {
            if (!checkAdmin())
                return (View("~/Views/Shared/Unauthorized.cshtml"));

            ViewBag.categoryData = UtilityFunctions.getCategoryDropDown();
            AdminBLL obj = new AdminBLL();
            string AppID;
            bool result = obj.addApparel(model, out AppID);
            if (result)
            {
                ViewBag.Message = "Sucessfully added apparel";
            }
                
            else
                ViewBag.Message = "Unable to add apparel";

            return View(model);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public string UpdateOffer(string offerDate, string offerName, string offerDescription, string offerDiscount)
        {
            if (!checkAdmin())
                return ("<h1 style='Color:red'>You Are NOT AUTHORIZED to access this Page ..! <h1>");

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
            if (!checkAdmin())
                return ("<h1 style='Color:red'>You Are NOT AUTHORIZED to access this Page ..! <h1>");

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
            if (!checkAdmin())
                return (View("~/Views/Shared/Unauthorized.cshtml"));

            AdminBLL obj = new AdminBLL();
            List<DropDownFormat> result = obj.getBrandNames();
            SelectList resList = new SelectList(result, "value", "name");
            return Json(resList);
            
        }
        

        [HttpPost]
        public ActionResult getApparelNameByBrand(string brand)
        {
            if (!checkAdmin())
                return (Content("<h1 style='Color:red'>You Are NOT AUTHORIZED to access this Page ..! <h1>"));

            AdminBLL obj = new AdminBLL();
            List<DropDownFormat> result = obj.getApparelNameByBrand(brand);
            SelectList resList = new SelectList(result, "value", "name");
            return Json(resList);
            
        }


        [HttpPost]
        public ActionResult getApparelsByBrandAndName(string name, string brand)
        {
            if (!checkAdmin())
                return (View("~/Views/Shared/Unauthorized.cshtml"));

            AdminBLL obj = new AdminBLL();
            List<AddApparel> listApparel = obj.getApparelsByBrandAndName(name, brand);
            return PartialView("_updateApparel", listApparel);

        }

        [HttpPost]
        public bool UpdateApparel(string apparelID, string  cost, string discount, string quantity, string category, string size )
        {
            if (!checkAdmin())
                return (false);

            AdminBLL obj = new AdminBLL();
            bool result = obj.UpdateApparel(apparelID, cost, discount, quantity, category, size);
            return result;

        }

        [HttpGet]
        public ActionResult UploadImage(string id)
        {
            if (!checkAdmin())
                return (Content("<h1 style='Color:red'>You Are NOT AUTHORIZED to access this Page ..! <h1>"));

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult UploadImage()
        {
            if (!checkAdmin())
                return (Json("<h1 style='Color:red'>You Are NOT AUTHORIZED to access this Page ..! <h1>"));

            string imgPath = string.Empty;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                //Use the following properties to get file's name, size and MIMEType
                int fileSize = file.ContentLength;
                string fileName = file.FileName;
                string mimeType = file.ContentType;
                System.IO.Stream fileContent = file.InputStream;
                //To save file, use SaveAs method
                imgPath = AdminController.ImagePath+"TempImage.jpg";
                file.SaveAs(Server.MapPath("~/")+imgPath); //File will be saved in directory
            }
            return Json(imgPath);
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
