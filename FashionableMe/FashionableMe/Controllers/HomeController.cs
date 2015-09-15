using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionableMe.Utils;
using FashionableMe.DataAccessLayer;
using FashionableMe.Models;

namespace FashionableMe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AdminDal obj = new AdminDal();
            List<Apparel> lst = obj.getProductByCategory("Male");
            ViewBag.Message = lst.Count; //"Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult OfferOfDay()
        {
            ViewBag.Message = "Offer of the Day!!!";

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

        public CaptchaImageResult ShowCaptchaImage()
        {
            return new CaptchaImageResult();
        }

        public ActionResult HisPage()
        {
            return View();
        }

        public ActionResult HerPage()
        {
            return View();
        }

        public ActionResult KidPage()
        {
            return View();
        }

    }
}
