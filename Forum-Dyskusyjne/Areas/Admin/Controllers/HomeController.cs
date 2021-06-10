using System.Web.Mvc;

namespace Forum_Dyskusyjne.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        // TODO: Create HTML Tags allowed crud
        // TODO: Create disallowed words crud
    }
}