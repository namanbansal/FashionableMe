using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionableMe.Models;
using FashionableMe.BLL;

namespace FashionableMe.Controllers
{
    //[Authorize]
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
            if (Session["UserID"] == null)
                return RedirectToAction("Index", "Home");

            if (Session["UserRole"] == "admin")
                return RedirectToAction("Index", "Admin");
            
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            if (Session["cart"] == null)
            {
                ViewBag.CartCount = "0";
            }
            else if (cart.Count<1)
            {
                ViewBag.CartCount = "0";
            }
            return View("Cart");
        }

        //
        // GET: /Cart/Details/5
        [HttpPost]
        public ActionResult OrderNow(string id, string size)
        {
            size = size.Trim();
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Account");
            if (Session["UserRole"] == "admin")
                return RedirectToAction("Index", "Admin");
            
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
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult OrderNowOffer(string id, string size, string offerDiscount, string offerID)
        {
            size = size.Trim();
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Account");
            if (Session["UserRole"] == "admin")
                return RedirectToAction("Index", "Admin");
            
            
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
            return RedirectToAction("Index");
        }

        public bool verifyQuantity()
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            bool isUpdated = false;
            for (int i = 0; i < cart.Count; i++)
            {
                int available = bllObj.checkAvailableQuantity(cart[i].Apparel.ApparelID, cart[i].Apparel.ApparelSize, cart[i].Quantity);
                if (available != cart[i].Quantity)
                {
                    cart[i].Quantity = available;
                    isUpdated = true;
                }
            }
            if (isUpdated)
            {
                Session["cart"] = cart;
                Session["cartUpdated"] = "true";
            }
            return isUpdated;
        }

        public ActionResult Shipping()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Account");
            if (Session["UserRole"] == "admin")
                return RedirectToAction("Index", "Admin");
            
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            if (cart.Count<1)
            {
                return RedirectToAction("Index");
            }
            
            bool isUpdated = verifyQuantity();

            bool isZero = RemoveZeroQuantity();
            if (isUpdated || isZero)
                return RedirectToAction("Index");
            
            DetailsViewModel custDetails = new DetailsViewModel();

            if (Session["UserID"] != null)

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
            if (Session["UserRole"] == "admin")
                return RedirectToAction("Index", "Admin");
            
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
            bool dummy = RemoveZeroQuantity();
            return status;
        }

        [HttpPost]
        public string ConfirmOrder(string UserID)
        {
            DetailsViewModel custDetails = new DetailsViewModel();
            custDetails = (DetailsViewModel)Session["MyOrder"];
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            
            List<MyOrder> orders = new List<MyOrder>();
            
            for (int i = 0; i < cart.Count; i++)
            {
                MyOrder order = new MyOrder();
                order.ShippingAddress = custDetails.Address;
                order.City = custDetails.City;
                order.State = custDetails.State;
                order.Pincode = custDetails.Pincode;
                order.UserID = UserID;
                order.ApparelID = cart[i].Apparel.ApparelID;
                order.ProductName = cart[i].Apparel.ApparelName;
                order.SizeOfApparel = cart[i].Apparel.ApparelSize;
                order.Quantity = cart[i].Quantity;
                order.TotalAmount = cart[i].Quantity * (cart[i].Apparel.ApparelCost - (cart[i].Apparel.ApparelCost * cart[i].Apparel.ApparelDiscount)/100);
                orders.Add(order);
            }
            string status = bllObj.InsertOrderDetails(orders);
            if (!status.Equals("false"))
                Session["cart"] = null;
            return status;
        }

        [HttpPost]
        public ActionResult PayByCredit(string totalAmount, string userID)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Account");
            if (Session["UserRole"] == "admin")
                return RedirectToAction("Index", "Admin");
            
            ViewBag.UserID = userID;
            ViewBag.TotalAmount = totalAmount;

            return View();
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
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Account");
            if (Session["UserRole"] == "admin")
                return RedirectToAction("Index", "Admin");
            
            int index = isExisting(id, size);
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            cart.RemoveAt(index);
            return RedirectToAction("Index");
        }

        public bool RemoveZeroQuantity()
        {
            bool isUpdated = false;
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Quantity < 1)
                {
                    cart.RemoveAt(i);
                    isUpdated = true;
                }
            }
            if (isUpdated)
            {
                Session["cart"] = cart;
                Session["cartUpdated"] = "true";
            }
            return isUpdated;
        }

        //
        // POST: /Cart/Delete/5

        
    }
}
