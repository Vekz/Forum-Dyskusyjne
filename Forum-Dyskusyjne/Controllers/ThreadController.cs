using System.Web.Mvc;
using Forum_Dyskusyjne.DAL;

namespace Forum_Dyskusyjne.Controllers
{
    public class ThreadController : Controller
    {
        private ForumDBContext db = new ForumDBContext();

        public ActionResult Index(int index)
        {
            var forum = db.Threads.Find(index);
            
            return View(forum);
        }
    }
}