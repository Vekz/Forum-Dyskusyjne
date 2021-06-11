using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Forum_Dyskusyjne.DAL;
using Forum_Dyskusyjne.Models;

namespace Forum_Dyskusyjne.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ThreadsController : Controller
    {
        private ForumDbContext db = new ForumDbContext();

        // GET: Threads
        public ActionResult Index()
        {
            var threads = db.Threads.Include(t => t.Author).Include(t => t.Category);
            return View(threads.ToList());
        }

        // GET: Threads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        // GET: Threads/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Threads/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ThreadId,ThreadTitle,IsPinned,AuthorId,CategoryId")] Thread thread)
        {
            if (ModelState.IsValid)
            {
                db.Threads.Add(thread);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", thread.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", thread.CategoryId);
            return View(thread);
        }

        // GET: Threads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", thread.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", thread.CategoryId);
            return View(thread);
        }

        // POST: Threads/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ThreadId,ThreadTitle,IsPinned,AuthorId,CategoryId")] Thread thread)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thread).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", thread.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", thread.CategoryId);
            return View(thread);
        }

        // GET: Threads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        // POST: Threads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Thread thread = db.Threads.Find(id);
            db.Threads.Remove(thread);
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
