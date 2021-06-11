using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Forum_Dyskusyjne.DAL;
using Forum_Dyskusyjne.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Forum_Dyskusyjne.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private ForumDbContext db = new ForumDbContext();
        private ApplicationUserManager userManager;
        private User defaultUser;
        
        public UsersController()
        {
            userManager = new ApplicationUserManager(new UserStore<User>(db));
            defaultUser = userManager.FindByEmail("NaN");
        }

        // GET: Users
        public ActionResult Index()
        {
            ViewBag.RoleList = db.Roles.ToList();
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return RedirectToAction("RegisterWithoutLogin", "Account", new { area = "" });
        }

        // GET: Users/Edit/5
        public ActionResult Edit(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (user == defaultUser)
            {
                return RedirectToAction("Index");
            }
            
            ViewBag.RoleList = new SelectList(db.Roles.ToList(), "Name", "Name");
            
            return View(user);
        }

        // POST: Users/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user, String role)
        {
            if (ModelState.IsValid)
            {
                // Stamp so the identity doesn't crash
                user.SecurityStamp = Guid.NewGuid().ToString();
                
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                
                // Delete all roles from user and add new one
                userManager.GetRoles(user.Id).ForEach(e => userManager.RemoveFromRole(user.Id, e));
                userManager.AddToRole(user.Id, role);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/id
        public ActionResult Delete(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (user == defaultUser)
            {
                return RedirectToAction("Index");
            }
            
            return View(user);
        }

        // POST: Users/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(String id)
        {
            User user = db.Users.Find(id);

            var postsWritten = user.Posts;
            var threadsCreated = user.Threads;
            var messagesSent = user.MessagesSent;
            var messagesReceived = user.MessagesReceived;

            postsWritten.ForEach(e =>
            {
                e.Author = defaultUser;
                e.AuthorId = defaultUser.Id;
            });
            
            threadsCreated.ForEach(e =>
            {
                e.Author = defaultUser;
                e.AuthorId = defaultUser.Id;
            });
            
            messagesSent.ForEach(e =>
            {
                e.Sender = defaultUser;
                e.SenderId = defaultUser.Id;
            });
            
            messagesReceived.ForEach(e =>
            {
                e.Receiver = defaultUser;
                e.ReceiverId = defaultUser.Id;
            });

            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
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
