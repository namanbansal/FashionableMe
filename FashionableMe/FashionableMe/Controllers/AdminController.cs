using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionableMe.BLL;
using FashionableMe.Models;
using FashionableMe.Utils;


namespace FashionableMe.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        UtilityFunctions util = new UtilityFunctions();
        public ActionResult Index()
        {
           
            
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
            defaultList[0].Selected= false;
            
            defaultList[Convert.ToInt32(ModelState["temp"])].Selected = true;
            ViewBag.categoryData = defaultList;
            
            if (obj.addOffer(model))
                ViewBag.Message = "Offer Added Successfully";
            else
                ViewBag.Message = "Unable to add Offer";
            ViewBag.Message = Convert.ToInt32(ModelState["temp"]);
            return View(model); ;
        }

       [HttpPost]
        public ActionResult searchByCategory(string val)
        {
            AdminBLL obj = new AdminBLL();
            List<Apparel> listApparel =  obj.fetchProductByCategory(Convert.ToInt16(val));
            //ViewBag.Message = listApparel[0].ApparelName;
            string msg="0";
           if(listApparel.Count>0)
                msg = listApparel[0].ApparelName;
            return PartialView("_apparelDetailsTable", listApparel) ;
        }
        

        

        public ActionResult addApparel()
        {
            ViewBag.categoryData = util.getCategoryDropDown();
            return View();
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
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
