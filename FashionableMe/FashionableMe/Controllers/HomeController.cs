using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionableMe.Utils;
using FashionableMe.DataAccessLayer;
using FashionableMe.BLL;
using FashionableMe.Models;

namespace FashionableMe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CustomerBLL obj = new CustomerBLL();
            List<Offer> offer = new List<Offer>();
            offer = obj.getTodaysOffer();
            if (offer.Count == 0)
            {
                ViewBag.OfferTitle = "none";

            }
            else
            {
                ViewBag.OfferTitle = offer[0].OfferName;
                ViewBag.OfferDescription = offer[0].OfferDescription;

            }
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
            return RedirectToAction("Him", "Customer");
        }

        public ActionResult HerPage()
        {
            return RedirectToAction("Her", "Customer");
        }

        public ActionResult KidPage()
        {
            return RedirectToAction("Kids", "Customer");
        }

    }
}
