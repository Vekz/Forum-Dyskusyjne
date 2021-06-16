using System.Threading.Tasks;
using System.Web.Mvc;
using Forum_Dyskusyjne.DAL;
using Forum_Dyskusyjne.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Collections;
using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Forum_Dyskusyjne.Controllers
{
    public class ThreadController : Controller
    {
        private ForumDbContext db = new ForumDbContext();

        public ActionResult Index(int index)
        {
            Thread thread = db.Threads.Find(index);
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


        [Authorize]
        public async Task<ActionResult> Report(int threadID , int postID)
        {
            var AdminRoleID = db.Roles.Where(r => r.Name.Equals("Admin")).Single().Id;
            List<User> admins = db.Users.Where(u => u.Roles.FirstOrDefault().RoleId == AdminRoleID).ToList(); ;
            User reportee = db.Users.Find(User.Identity.GetUserId());
            string title = "Report by user: " + User.Identity.GetUserName() + " on thread:" + db.Threads.Find(threadID).ThreadTitle;
            string content = " Link do wątku: " +  Url.Action("Details", "Threads",new { area = "Admin", id = postID }) +"\n" + "Treść postu: " + db.Posts.Find(postID).Body;
            foreach (User adm in admins)
            {
                Message report = new Message()
                {
                    SendDate = DateTime.Now,
                    Sender = reportee,
                    Title = title,
                    Text = content,
                    Receiver = adm,
                    OrginalSender = reportee.UserName,
                    OrginalReciver = adm.UserName,
                    Seen = false,
                };
                db.Messages.Add(report);
            }
            await db.SaveChangesAsync();

            return RedirectToAction("Index", new { index = threadID });
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