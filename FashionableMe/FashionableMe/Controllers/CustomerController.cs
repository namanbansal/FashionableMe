using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionableMe.BLL;
using FashionableMe.Models;
using System.Web.Script.Serialization;
using FashionableMe.DataAccessLayer;

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

        public ActionResult OfferOfDay()
        {
            ViewBag.Title = "Offer of The Day!!!";
            CustomerBLL obj = new CustomerBLL();
            List<Offer> model = new List<Offer>();
            model = obj.getTodaysOffer();
            Apparel apparel = new Apparel();
            apparel = obj.getApparelByID(model[0].ApparelID)[0];
            ViewBag.OfferID = model[0].OfferID;
            ViewBag.OfferName = model[0].OfferName;
            ViewBag.OfferDescription = model[0].OfferDescription;
            ViewBag.OfferDiscount = model[0].Discount;
            ViewBag.OfferName = model[0].OfferName;
            return View(apparel);

        }

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

        [HttpPost]
        public ActionResult getRatingDetails(string apparelID)
        {
            CustomerDal obj = new CustomerDal(); 
            return(Json(obj.getRatingDetails(apparelID),JsonRequestBehavior.AllowGet));
        }

        [HttpPost]
        public ActionResult UpdateRatingDetails(string apparelID , string userID , int rating , string comment, int currRating)
        {
            CustomerDal obj = new CustomerDal();
            bool status = obj.InsertOrUpdateRating(apparelID , userID , rating , comment,currRating);
            return(Json(new {state = status},JsonRequestBehavior.AllowGet));
        }

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}


        [HttpPost]
        public bool setToSession(ItemDetails item)
        {
            Session["ItemDetails"] = item;
            return true;
        }

        [HttpPost]
        public ActionResult getFromSession(string sender)
        {
            ItemDetails item = (ItemDetails)Session["ItemDetails"];
            Session["ItemDetails"] = null;
            return(Json(item));
        }

        //
        // GET: /Customer/Create

        public ActionResult MyOrders()
        {
            CustomerBLL bllobj = new CustomerBLL();
            List<MyOrder> listOrders = new List<MyOrder>();
            string userID = Session["UserID"].ToString();
            listOrders = bllobj.GetOrderDetails(userID);
            return View(listOrders);
        }


     
        


        
    }
}
