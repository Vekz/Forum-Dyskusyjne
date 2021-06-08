using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Forum_Dyskusyjne.DAL;

namespace Forum_Dyskusyjne.Controllers
{
    public class HomeController : Controller
    {
        private ForumDBContext db = new ForumDBContext();
        
        public ActionResult Index()
        {
            var categories = db.Categories.Include(c => c.ParentCategory);
            return View(categories.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}