using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionableMe.BLL;
using FashionableMe.Models;


namespace FashionableMe.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
           
            
            return View();
        }

        //
        // GET: /Admin/Details/5

        public ActionResult Offer()
        {
            List<SelectListItem> categoryTypes = new List<SelectListItem>();
            categoryTypes.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
            categoryTypes.Add(new SelectListItem { Text = "Male", Value = "1" });
            categoryTypes.Add(new SelectListItem { Text = "Female", Value = "2" });
            categoryTypes.Add(new SelectListItem { Text = "Kids", Value = "3" });
            ViewBag.categoryData = categoryTypes;
            return View();
        }

        [HttpPost]
        public string searchByCategory(string val)
        {
            AdminBLL obj = new AdminBLL();
            List<Apparel> listApparel =  obj.fetchProductByCategory(val);
            return val ;
        }
        

        //
        // GET: /Admin/Edit/5

        public ActionResult Edit(int id)
        {
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
