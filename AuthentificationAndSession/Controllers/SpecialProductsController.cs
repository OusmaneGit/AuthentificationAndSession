using System;
using System.Web.Mvc;
using AuthentificationAndSession.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AuthentificationAndSession.Controllers
{
    [Authorize(Roles = "Merchand, Client")]
    public class SpecialProductsController : Controller
    {
        public ActionResult AngularProducts()
        {
            return View("AngularProducts", "", GetSpecialProducts());
        }

        private string GetSpecialProducts()
        {
            var SpecProd = new[]
            {
                new SpecialProducts { Id= "999", Name= "I have no idea 1", From =  DateTime.Now.Date.AddDays(1).ToString("dd/MM/yyyy"), Price = Convert.ToDecimal(15.99), Duration= "2h" },
                new SpecialProducts { Id= "998", Name= "I have no idea 2", From =  DateTime.Now.Date.AddDays(2).ToString("dd/MM/yyyy"), Price = Convert.ToDecimal(25.99), Duration= "45m" },
                new SpecialProducts { Id= "997", Name= "I have no idea 3", From =  DateTime.Now.Date.AddDays(3).ToString("dd/MM/yyyy"), Price = Convert.ToDecimal(35.99), Duration= "60m" },
                new SpecialProducts { Id= "996", Name= "I have no idea 4", From =  DateTime.Now.Date.AddDays(4).ToString("dd/MM/yyyy"), Price = Convert.ToDecimal(45.99), Duration= "30m" }
            };
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            return JsonConvert.SerializeObject(SpecProd, Formatting.None, settings);
        }

    }
}
