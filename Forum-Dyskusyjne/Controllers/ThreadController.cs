using System.Threading.Tasks;
using System.Web.Mvc;
using Forum_Dyskusyjne.DAL;
using Forum_Dyskusyjne.Models;
using Microsoft.AspNet.Identity;

namespace Forum_Dyskusyjne.Controllers
{
    public class ThreadController : Controller
    {
        private ForumDbContext db = new ForumDbContext();

        public ActionResult Index(int index)
        {
            var thread = db.Threads.Find(index);
            if (thread == null)
            {
                return View("Error");
            }
            
            thread.Views++;
            db.SaveChanges();
            
            return View(thread);
        }

        [ChildActionOnly]
        [Authorize]
        public ActionResult MakePost()
        {
            return PartialView("_MakePost");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MakePost(MakePostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", db.Threads.Find(model.ThreadId));
            }
            else if (!this.TryValidateModel(model))
            {
                ModelState.AddModelError(nameof(MakePostViewModel.Content), "Post content contains prohibited HTML tags!");
                return View("Index", db.Threads.Find(model.ThreadId));
            }

            var post = new Post
            {
                Author = db.Users.Find(User.Identity.GetUserId()), Body = model.Content,
                Thread = db.Threads.Find(model.ThreadId)
            };
            db.Posts.Add(post);
            await db.SaveChangesAsync();

            return RedirectToAction("Index", new { index = model.ThreadId });
        }



        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}