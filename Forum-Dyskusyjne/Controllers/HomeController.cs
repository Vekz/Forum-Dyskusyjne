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
    }
}