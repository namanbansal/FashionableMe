using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using FashionableMe.Filters;
using FashionableMe.Models;
using FashionableMe.BLL;

namespace FashionableMe.Controllers
{
    //[Authorize]
    
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl)
        {
            string str = string.Empty;
            //str = Request.RawUrl.ToString();
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AccountBLL accBLL = new AccountBLL();
                if (accBLL.checkLoginDetails(model))
                {
                    if (Convert.ToString(Session["UserRole"]) == "admin")
                    {
                        return RedirectToAction("Offer", "Admin");
                    }

                    return RedirectToLocal(returnUrl);
                }
            }
            
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["SessionUser"] = null;
            Session["UserRole"] = null;
            Session["cart"] = null;

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.Message = "";
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customer model, string CaptchaText)
        {
            if (ModelState.IsValid)
            {
                if (CaptchaText == HttpContext.Session["captchastring"].ToString())
                {
                    //ViewBag.Message = "CAPTCHA verification successful!";
                    AccountBLL accBLL = new AccountBLL();
                    if (accBLL.sendRegisterDetails(model))
                    {
                        ViewBag.Message = "User added successfully!!!";
                        return View(model);
                    }
                }
                else
                {
                    ViewBag.Message = "Captcha did not Match! Try Again.";
                    return View(model);
                }
             
            }

            // If we got this far, something failed, redisplay form
            ViewBag.Message = "Unable to Register!!!";
            return View(model);
        }

        

        //
        // GET: /Account/Details
        
        public ActionResult Details()
        {
            AccountBLL accBLL = new AccountBLL();
            if (Session["SessionUser"] == null)
            {
                return(RedirectToAction("Login","Account"));
            }

            DetailsViewModel model = accBLL.getCustomerDetails(HttpContext.Session["SessionUser"].ToString());
            ViewBag.statusCode = 0;
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Details(DetailsViewModel model)
        {
            int statusCode = 0;
            string status = new AccountBLL().CheckAndUpdateCustomer(model,out statusCode);
            ViewBag.Message = status;
            ViewBag.statusCode = statusCode;
            return View();

        }
        

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
