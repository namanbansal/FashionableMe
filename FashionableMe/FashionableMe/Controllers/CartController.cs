using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionableMe.Models;
using FashionableMe.BLL;

namespace FashionableMe.Controllers
{
    public class CartController : Controller
    {
        private CartBLL bllObj;
        public CartController()
        {
            bllObj = new CartBLL();
        }

        //
        // GET: /Cart/
        public ActionResult Index()
        {
            return View("Cart");
        }

        //
        // GET: /Cart/Details/5
        public ActionResult OrderNow(string id, string size)
        {
            size = size.Trim();
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Account");

            if (Session["cart"] == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem(bllObj.getApparelForCart(id, size), 1, "NOOFF"));
                Session["cart"] = cart;
            }
            else
            {
                List<CartItem> cart = (List<CartItem>)Session["cart"];
                int index = isExisting(Convert.ToInt32(id), size);
                if (index == -1)
                    cart.Add(new CartItem(bllObj.getApparelForCart(id, size), 1, "NOOFF"));
                else
                    cart[index].Quantity++;
                Session["cart"] = cart;
            }
            return View("Cart");
        }

        public ActionResult OrderNowOffer(string id, string size, string offerDiscount, string offerID)
        {
            size = size.Trim();
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Account");

            Apparel apparel = new Apparel();
            apparel = bllObj.getApparelForCart(id, size);
            apparel.ApparelDiscount += Convert.ToDecimal(offerDiscount);
            if (Session["cart"] == null)
            {
                List<CartItem> cart = new List<CartItem>();

                cart.Add(new CartItem(apparel, 1, offerID));
                Session["cart"] = cart;
            }
            else
            {
                List<CartItem> cart = (List<CartItem>)Session["cart"];
                int index = isExisting(Convert.ToInt32(id), size);
                if (index == -1)
                    cart.Add(new CartItem(apparel, 1, offerID));
                else
                    cart[index].Quantity++;
                Session["cart"] = cart;
            }
            return View("Cart");
        }

        public ActionResult Shipping()
        {
            DetailsViewModel custDetails = new DetailsViewModel();
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Account");
            else
            {
                custDetails = bllObj.getShippingDetails(Session["UserID"].ToString());

            }

            return View(custDetails);
        }

        [HttpPost]
        public ActionResult Shipping(DetailsViewModel custDetails)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Account");
            ViewBag.UserID = Session["UserID"];
            Session["MyOrder"] = custDetails;
            return View("CheckOut", custDetails);
        }

        [HttpPost]
        public bool updateCart(string id, string size, int quantity)
        {
            size = size.Trim();
            bool status=false;
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            int index = isExisting(Convert.ToInt32(id), size);
            if (index == -1)
                //cart.Add(new CartItem(bllObj.getApparelForCart(id, size), 1));
                ViewBag.Message = "Unable to Update Cart!";
            else
            {
                cart[index].Quantity = quantity;
                ViewBag.Message = "Cart Updated!";
                status = true;

            }
            Session["cart"] = cart;
            return status;
        }

        [HttpPost]
        public bool ConfirmOrder(string UserID)
        {
            DetailsViewModel custDetails = new DetailsViewModel();
            custDetails = (DetailsViewModel)Session["MyOrder"];
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            List<MyOrder> orders = new List<MyOrder>();
            MyOrder order = new MyOrder();
            order.ShippingAddress = custDetails.Address;
            order.City = custDetails.City;
            order.State = custDetails.State;
            order.Pincode = custDetails.Pincode;
            order.UserID = UserID;
            for (int i = 0; i < cart.Count; i++)
            {
                order.ProductName = cart[i].Apparel.ApparelName;
                order.SizeOfApparel = cart[i].Apparel.ApparelSize;
                order.Quantity = cart[i].Quantity;
                order.TotalAmount = cart[i].Quantity * (cart[i].Apparel.ApparelCost - (cart[i].Apparel.ApparelCost * cart[i].Apparel.ApparelDiscount)/100);
                orders.Add(order);
            }
            return bllObj.InsertOrderDetails(orders);
        }

        private int isExisting(int id, string size)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];

            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Apparel.ApparelID == id && cart[i].Apparel.ApparelSize.Equals(size))
                    return i;
            }
            return -1;
        }
       
        //
        // GET: /Cart/Delete/5

        public ActionResult Delete(int id, string size)
        {
            int index = isExisting(id, size);
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            cart.RemoveAt(index);
            return View("Cart");
        }

        //
        // POST: /Cart/Delete/5

        
    }
}
