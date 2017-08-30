using System;
using System.Configuration;
using System.Data.SqlClient;

namespace AuthentificationAndSession.Controllers.DAL
{
    public static class CUtil
    {
        public static String GetConnexion()
        {
            string connexion = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            var builder = new SqlConnectionStringBuilder(connexion);
            return builder.ConnectionString;
        }
    }
}