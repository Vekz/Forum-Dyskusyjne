using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Forum_Dyskusyjne.DAL;
using Forum_Dyskusyjne.Models;
using Microsoft.AspNet.Identity;
using Thread = Forum_Dyskusyjne.Models.Thread;

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

        [Authorize]
        public ActionResult MakeThread(int index)
        {
            var th = new MakeThreadViewModel
            {
                CategoryId = index,
                FirstPostContent = "",
                ThreadTitle = ""
            };
            
            return View(th);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MakeThread(MakeThreadViewModel model)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var newThread = new Thread
                {
                    Author = db.Users.Find(User.Identity.GetUserId()),
                    Category = db.Categories.Find(model.CategoryId),
                    IsPinned = false,
                    ThreadTitle = model.ThreadTitle
                };
                db.Threads.Add(newThread);

                var firstPost = new Post
                {
                    Author = db.Users.Find(User.Identity.GetUserId()),
                    Body = model.FirstPostContent,
                    Thread = newThread
                };
                db.Posts.Add(firstPost);
                await db.SaveChangesAsync();

                return RedirectToAction("Index", new { index = model.CategoryId });
            }
            catch (DbEntityValidationException exception)
            {
                foreach (var exc in exception.EntityValidationErrors)
                {
                    continue;
                }
                
                return RedirectToAction("Index", new { index = model.CategoryId });
            }
        }
    }
}