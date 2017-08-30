using System.Web.Mvc;

namespace AuthentificationAndSession.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult MyError()
        {
            return View();
        }
    }
}
