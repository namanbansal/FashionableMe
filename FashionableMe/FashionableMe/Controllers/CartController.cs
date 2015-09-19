using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionableMe.Models;

namespace FashionableMe.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Cart/Details/5

        public ActionResult OrderNow(int id)
        {
            if (Session["cart"] == null)
            {
                List<CartItem> cart = new List<CartItem>();
                //cart.Add(new CartItem(
            }
            else
            {

            }
            return View();
        }

        //
        // GET: /Cart/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Cart/Create

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
        // GET: /Cart/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Cart/Edit/5

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
        // GET: /Cart/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Cart/Delete/5

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
