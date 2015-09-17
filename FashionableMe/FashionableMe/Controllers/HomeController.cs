﻿using System;
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
            Session["ErrorMessage"] = "No  ERROR Found ";
            //Apparel ap = new Apparel() { ApparelID = 101, ApparelName = "trouser", BrandName="van hussein",ApparelCost= 50M, Description= "very good",ApparelImage= "path", ApparelCategory="Female",ApparelAddTime= DateTime.Now,ApparelRating= 0,ApparelSize= "XL",QuantityPerSize = 10,ApparelDiscount= 5 };
            //new AdminDal().addProduct(ap);
            //ViewBag.Message = Session["ErrorMessage"]; //"Modify this template to jump-start your ASP.NET MVC application.";
            //var lst = new AdminDal().getProductByCategory("Female");
            //Offer obj = new Offer() {OfferName = "Diwali Dhamaka", OfferDescription="Bumper offer", ApparelID=101,OfferDate=DateTime.Now.Date, Discount=20.5M };
            AdminDal obj = new AdminDal();

            string Date = "3333-03-31";
            string date = Date.Substring(8, 2);
            string month = Date.Substring(5, 2);
            string year = Date.Substring(0, 4);
            string dateFormatted = month + "/" + date + "/" + year;
            string offerName = obj.getOfferDetails(dateFormatted).OfferName;
            ViewBag.Message = offerName;
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
