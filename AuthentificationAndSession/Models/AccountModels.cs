using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace AuthentificationAndSession.Models
{
    public class AccountModels
    {
        [XmlElement("UserName")]
        [Required(ErrorMessage = "* The login is required")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [XmlElement("Password")]
        [Required(ErrorMessage = "* The password is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [XmlElement("Role")]
        public string Role { get; set; }

        [XmlAttribute("Id")]
        public Int32 Id { get; set; }

        [Display(Name = "Remember me ?")]
        public bool RememberMe { get; set; }
    }
}