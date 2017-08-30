using System;
using System.Collections.Generic;

namespace AuthentificationAndSession.Models
{
    public class OrdersModels
    {
        public Int32 OrdersId { get; set; }
        public DateTime OrdersDate { get; set; }
        public List<ProductsModels> lstProducts { get; set; }
    }
}