using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using AuthentificationAndSession.Helpers;
using AuthentificationAndSession.Models;

namespace AuthentificationAndSession.Controllers
{
    [Log]
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(AccountModels model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var lst = AccountHelper.GetListOfUser(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["XmlPathUsers"]));
                if (lst != null && lst.Exists(x => x.UserName.Equals(model.UserName) && x.Password.Equals(model.Password)))
                {
                    Session.Clear();
                    var ticket = AccountHelper.CreateAuthenticationTicket(model.UserName, lst.Find(x => x.UserName.Equals(model.UserName) && x.Password.Equals(model.Password)).Role, false);
                    var encrypetedTicket = FormsAuthentication.Encrypt(ticket);
                    FormsAuthentication.SetAuthCookie(encrypetedTicket, false);
                    return RedirectToAction("GetAllProduct", "Products");
                }
                ModelState.AddModelError("", "Login or password incorrect");
                return View(model);
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("GetAllProducts", "Products");
        }

        
    }
}
