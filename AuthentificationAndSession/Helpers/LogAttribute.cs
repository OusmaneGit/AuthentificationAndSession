using System;
using System.Web.Mvc;

namespace AuthentificationAndSession.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class LogAttribute : ActionFilterAttribute
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //nothing
        }
    }
}