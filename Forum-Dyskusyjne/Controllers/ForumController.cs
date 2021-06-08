using System.Web.Mvc;
using Forum_Dyskusyjne.DAL;

namespace Forum_Dyskusyjne.Controllers
{
    public class ForumController : Controller
    {
        private ForumDBContext db = new ForumDBContext();

        public ActionResult Index(int index)
        {
            var forum = db.Categories.Find(index);
            
            return View(forum);
        }
    }
}