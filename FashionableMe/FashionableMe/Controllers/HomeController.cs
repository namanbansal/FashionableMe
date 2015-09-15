using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionableMe.DataAccessLayer;

namespace FashionableMe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AddProductData obj = new AddProductData();
            var msg = obj.addProduct();
            ViewBag.Message = msg; // "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
