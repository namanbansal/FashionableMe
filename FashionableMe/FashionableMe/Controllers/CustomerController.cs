using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionableMe.BLL;
using FashionableMe.Models;
using System.Web.Script.Serialization;

namespace FashionableMe.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Customer/Details/5

        public ActionResult Him()
        {
            ViewBag.Title = "Apparel for Him";
            CustomerBLL obj = new CustomerBLL();
            List<Apparel> model = new List<Apparel>();
            model = obj.getProductByCategory("Male");
            return View(model);

        }

        public ActionResult Her()
        {
            ViewBag.Title = "Apparel for Her";
            CustomerBLL obj = new CustomerBLL();
            List<Apparel> model = new List<Apparel>();
            model = obj.getProductByCategory("Female");
            return View(model);

        }

        public ActionResult Kids()
        {
            ViewBag.Title = "Apparel for Kids";
            CustomerBLL obj = new CustomerBLL();
            List<Apparel> model = new List<Apparel>();
            model = obj.getProductByCategory("Kids");
            return View(model);

        }

        [HttpPost]
        public ActionResult searchByCategory(string val)
        {
            CustomerBLL obj = new CustomerBLL();
            List<Apparel> listApparel = obj.getProductByCategory(val);
            //ViewBag.Message = listApparel[0].ApparelName;
            string msg = "0";
            if (listApparel.Count > 0)
                msg = listApparel[0].ApparelName;
            return PartialView("_apparelDetailsTable", listApparel);
        }

        [HttpPost]
        public string getQuantityDetails(string apparelID)
        {
            CustomerBLL obj = new CustomerBLL();
            List<Quantity> quantityDetails = obj.getQuantityDetails(apparelID);
            JavaScriptSerializer jsonobj = new JavaScriptSerializer();

            return (jsonobj.Serialize(quantityDetails));
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Customer/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Customer/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Customer/Edit/5

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
        // GET: /Customer/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Customer/Delete/5

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
