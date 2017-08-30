using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AuthentificationAndSession.Controllers.DAL;
using AuthentificationAndSession.Models;

namespace AuthentificationAndSession.Controllers
{
    public class ProductsController : Controller
    {
        //
        // GET: /Products/
        [AllowAnonymous]
        public ActionResult GetAllProducts()
        {
            return View(GetProducts().ToList());
        }

        [Authorize]
        public ActionResult GetAllProduct()
        {
            List<ProductsModels> lstProd = GetProducts();
            if (Session["allproducts"] == null)
                Session.Add("allproducts", lstProd);
           
            return View(lstProd.ToList());
        }

        [Authorize]
        [HttpPost]
        public JsonResult AddItemToCart(Int32 productId)
        {
            if (Session["allproducts"] == null)
            {
                var lstProducts = GetProducts();
                Session.Add("allproducts", lstProducts);
            }

            var lstGlobal = (List<ProductsModels>)Session["allproducts"];
            var lstAdded = new List<ProductsModels>();

            if (Session["addedproducts"] != null)
            {
                lstAdded = (List<ProductsModels>)Session["addedproducts"];
            }

            var addedItem = lstGlobal.Find(x => x.ProductsId == productId);
            lstAdded.Add(addedItem);
            Session["addedproducts"] = lstAdded;
            var totalValue = lstAdded.Sum(x => x.ProductsPrice);
            var nbItem = lstAdded.Count();

            return Json(String.Format("<div style=\"text-align:center\"> items : <b>{0}</b> Total : <b>{1}</b> €</div>", nbItem, totalValue), JsonRequestBehavior.DenyGet);
        }

        [Authorize]
        [HttpPost]
        public JsonResult RemoveItemFromCart(Int32 productId)
        {
            Decimal totalValue = 0;
            var nbItem = 0;

            if (Session["addedproducts"] != null)
            {
                ProductsModels removedItem = null;
                var lstAdded = (List<ProductsModels>)Session["addedproducts"];
                removedItem = lstAdded.Find(x => x.ProductsId == productId);
                if (removedItem != null)
                {
                    lstAdded.Remove(removedItem);
                    Session["addedproducts"] = lstAdded;
                }
                totalValue = lstAdded.Sum(x => x.ProductsPrice);
                nbItem = lstAdded.Count();
            }

            return Json(String.Format("<div style=\"text-align:center\"> items : <b>{0}</b> Total : <b>{1}</b> €</div>", nbItem, totalValue), JsonRequestBehavior.DenyGet);
        }

        public ActionResult Index()
        {
            return RedirectToAction(User.Identity.IsAuthenticated ? "GetAllProduct" : "GetAllProducts");
        }

        private List<ProductsModels> GetProducts()
        {
            List<ProductsModels> lstProduct;
            using (var dal = new ProductsDAL(CUtil.GetConnexion()))
            {
                lstProduct = dal.SelectAll().ToList();
            }
            return lstProduct;
        }
    }
}
