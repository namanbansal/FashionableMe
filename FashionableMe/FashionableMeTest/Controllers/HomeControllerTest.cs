using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NUnit.Framework;
using FashionableMe.Controllers;

namespace FashionableMeTest.Controllers
{
    [TestFixture]
    public class HomeControllerTest : Controller
    {
        [Test]
        public void IndexTest()
        {
            var sut = new HomeController();
            var result = sut.Index() as ViewResult;
            
            Assert.That(result.ViewBag.Message, Is.EqualTo("Modify this template to jump-start your ASP.NET MVC application."));
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
