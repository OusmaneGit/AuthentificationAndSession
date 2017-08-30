using System;
using System.ComponentModel.DataAnnotations;

namespace AuthentificationAndSession.Models
{
    public class ProductsModels
    {
        [Display(Name = "Name")]
        public String ProductsName { get; set; }
        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:0.00# €}")]
        public Decimal ProductsPrice { get; set; }
        public Int32 ProductsId { get; set; }
    }
}