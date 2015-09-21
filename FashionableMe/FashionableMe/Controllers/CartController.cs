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
            if (Session["cart"] == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem(bllObj.getApparelForCart(id, size), 1));
                Session["cart"] = cart;
            }
            else
            {
                List<CartItem> cart = (List<CartItem>)Session["cart"];
                int index = isExisting(Convert.ToInt32(id), size);
                if (index == -1)
                    cart.Add(new CartItem(bllObj.getApparelForCart(id, size), 1));
                else
                    cart[index].Quantity++;
                Session["cart"] = cart;
            }
            return View("Cart");
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
