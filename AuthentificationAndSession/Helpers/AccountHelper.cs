using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Xml.Serialization;
using AuthentificationAndSession.Models;

namespace AuthentificationAndSession.Helpers
{
    public static class AccountHelper
    {
        public static List<AccountModels> GetListOfUser(string pathXml)
        {
            var LstUsers = new List<AccountModels>();
            var deserializer = new XmlSerializer(typeof(List<AccountModels>));
            using (TextReader textReader = new StreamReader(pathXml))
            {
                LstUsers = (List<AccountModels>)deserializer.Deserialize(textReader);
                textReader.Close();
            }
            return LstUsers;
        }

        public static string GetUserName()
        {
            var id = (FormsIdentity)HttpContext.Current.User.Identity;
            var ticket = (id.Ticket);
            ticket = FormsAuthentication.Decrypt(id.Ticket.Name);
            var name = ticket.Name;
            return name;
        }

        public static FormsAuthenticationTicket CreateAuthenticationTicket(string userName, string commaSeperatedRoles, bool createPersistentCookie)
        {
            var cookiePath = FormsAuthentication.FormsCookiePath;
            const int expirationMinutes = 30;
            var ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(expirationMinutes), createPersistentCookie, commaSeperatedRoles, cookiePath);
            return ticket;
        }
    }
}