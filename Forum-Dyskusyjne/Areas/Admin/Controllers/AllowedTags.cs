using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using Forum_Dyskusyjne.Areas.Utils;
using Forum_Dyskusyjne.Models;
using Ganss.XSS;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

using FileIO = System.IO.File;

namespace Forum_Dyskusyjne.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AllowedTagsController : Controller
    {
        public static readonly string JsonPath = HostingEnvironment.MapPath("~/App_Data/allowedtags.json");
        private static List<string> _allowedTags = JsonUtils.ReadStringListFromJson(JsonPath);

        // GET: AllowedTags
        public ActionResult Index()
        {
            return View(_allowedTags);
        }

        [ChildActionOnly]
        public ActionResult AddTag()
        {
            return PartialView("_AddTag");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTag(string word)
        {
            if (!ModelState.IsValid || String.IsNullOrWhiteSpace(word) || word.Any(Char.IsWhiteSpace))
            {
                ModelState.AddModelError("AllowedTag", "Allowed tags cannot be empty or contain whitespaces!");
                return View("Index", _allowedTags);
            }
            else if (_allowedTags.Contains(word))
            {
                ModelState.AddModelError("AllowedTag", "Allowed tag is already on list!");
                return View("Index", _allowedTags);
            }

            _allowedTags.Add(word);
            JsonUtils.SaveListToJson(JsonPath, _allowedTags);

            return RedirectToAction("Index", "AllowedTags", new{ area = "Admin" });
        }

        // POST: AllowedTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id < 0 || id >= _allowedTags.Count)
            {
                return HttpNotFound();
            }

            _allowedTags.RemoveAt(id);
            JsonUtils.SaveListToJson(JsonPath, _allowedTags);
            
            return RedirectToAction("Index");
        }
    }
}