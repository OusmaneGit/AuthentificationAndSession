using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using AuthentificationAndSession.Controllers.DAL;
using AuthentificationAndSession.Helpers;
using AuthentificationAndSession.Models;

namespace AuthentificationAndSession.Controllers
{
    public class OrdersController : Controller
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CheckOut()
        {
            return RedirectToAction("CheckOut");
        }

        [Authorize]
        [ActionName("CheckOut")]
        public ActionResult CheckOutReload()
        {
            var lstProduct = new List<ProductsModels>();
            if (Session["addedproducts"] != null)
            {
                lstProduct = (List<ProductsModels>)Session["addedproducts"];
            }

            var orders = new OrdersModels()
            {
                OrdersId = 0,
                OrdersDate = DateTime.Now,
                lstProducts = lstProduct
            };
            return View(orders);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Save()
        {
            var lstProduct = new List<ProductsModels>();
            if (Session["addedproducts"] != null)
            {
                lstProduct = (List<ProductsModels>)Session["addedproducts"];
            }

            Session.Remove("addedproducts");
            var orders = new OrdersModels()
            {
                OrdersId = 0,
                OrdersDate = DateTime.Now,
                lstProducts = lstProduct
            };

            using (var scope = new TransactionScope())
            {
                using (var dal = new OrdersDAL(CUtil.GetConnexion()))
                {
                    var OrdersId = dal.OrdersInsert(AccountHelper.GetUserName(), orders.OrdersDate);
                    foreach (var item in orders.lstProducts)
                    {
                        dal.OrdersDetailsInsert(OrdersId, item.ProductsId);
                    }
                }

                scope.Complete();
            }


            return RedirectToAction("MyOrders");
        }

        [Authorize]
        public ActionResult MyOrders()
        {
            var lstOrders = new List<OrdersModels>();
            using (var dal = new OrdersDAL(CUtil.GetConnexion()))
            {
                lstOrders = dal.SelectFromUserName(AccountHelper.GetUserName()).ToList();
                foreach (var item in lstOrders)
                {
                    item.lstProducts = dal.SelectProductsFromOrdersId(item.OrdersId).ToList();
                }
            }

            return View(lstOrders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateFile(FormCollection frm)
        {
            var OrderId = Convert.ToInt32(frm["Id"]);
            var orders  = new List<OrdersModels>();
            using (var dal = new OrdersDAL(CUtil.GetConnexion()))
            {
                orders = dal.SelectFromUserName(AccountHelper.GetUserName()).ToList();
                foreach (var item in orders)
                {
                    item.lstProducts = dal.SelectProductsFromOrdersId(item.OrdersId).ToList();
                }
            }

            var OrdersToWrite = orders.Find(x => x.OrdersId == OrderId);
            var pathOfTheGeneratedFile = GenerateTextFile(OrdersToWrite);


            System.Net.Mime.ContentDisposition cd = null;
            string path = null;
            foreach (var item in pathOfTheGeneratedFile)
            {
                cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = item.Value,
                    Inline = false,
                };
                path = item.Key;
            }

            byte[] bytes = System.IO.File.ReadAllBytes(path);
            var stream = new MemoryStream(bytes);
            return File(stream, "text/plain", cd.FileName);   
        }

        public Dictionary<String, String> GenerateTextFile(OrdersModels Orders)
        {
            var nameOfMyFile = DateTime.Now.Ticks.ToString() +  Orders.OrdersId + ".txt";
            var file = new StreamWriter(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["CaptureFilePath"] + nameOfMyFile), true);
            file.WriteLine("---------------------------------------------------------------------------");
            file.WriteLine("OrderId : {0}", Orders.OrdersId);
            file.WriteLine("Date : {0}", Orders.OrdersDate.ToString("dd/MM/yyyy"));
            file.WriteLine("");
            file.WriteLine("Your products : ");

            foreach (var item in Orders.lstProducts)
            {
                file.WriteLine("Id : {0} - Name : {1} - Price : {2}", item.ProductsId, item.ProductsName, item.ProductsPrice);
            }

            file.WriteLine("");
            file.WriteLine("Total product : {0}", Orders.lstProducts.Count());
            file.WriteLine("Total price : {0}", Orders.lstProducts.Sum(x => x.ProductsPrice));
            file.WriteLine("---------------------------------------------------------------------------");
            file.Close();
            file.Dispose();

            var dict = new Dictionary<string, string>
                {
                    {
                        HttpContext.Server.MapPath(ConfigurationManager.AppSettings["CaptureFilePath"] + nameOfMyFile),
                        nameOfMyFile
                    }
                };
            return dict;
        }
    }
}
