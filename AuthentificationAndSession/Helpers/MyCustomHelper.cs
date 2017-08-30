using System.Linq;
using System.Web.Mvc;

namespace AuthentificationAndSession.Helpers
{
    public static class MyCustomHelper
    {
        public static string PutActiveTabMenu(this HtmlHelper helper, string activeController, string[] activeActions, string cssClass)
        {
            var currentAction = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();
            var currentController = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
            var cssClassToUse = currentController == activeController &&
                                   activeActions.Contains(currentAction)
                                   ? cssClass
                                   : string.Empty;
            return cssClassToUse;
        } 
    }
}