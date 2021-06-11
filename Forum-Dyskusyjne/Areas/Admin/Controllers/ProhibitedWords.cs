using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using Forum_Dyskusyjne.Areas.Utils;
using Forum_Dyskusyjne.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

using FileIO = System.IO.File;

namespace Forum_Dyskusyjne.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProhibitedWordsController : Controller
    {
        public static readonly string JsonPath = HostingEnvironment.MapPath("~/App_Data/prohibitedwords.json");
        private static List<string> _prohibitedWords = JsonUtils.ReadStringListFromJson(JsonPath);

        // GET: ProhibitedWords
        public ActionResult Index()
        {
            return View(_prohibitedWords);
        }

        [ChildActionOnly]
        public ActionResult AddWord()
        {
            return PartialView("_AddWord");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddWord(string word)
        {
            if (!ModelState.IsValid || String.IsNullOrWhiteSpace(word) || word.Any(Char.IsWhiteSpace))
            {
                ModelState.AddModelError("ProhibitedWord", "Prohibited word cannot be empty or contain whitespaces!");
                return View("Index", _prohibitedWords);
            }
            else if (_prohibitedWords.Contains(word))
            {
                ModelState.AddModelError("ProhibitedWord", "Prohibited word is already on list!");
                return View("Index", _prohibitedWords);
            }

            _prohibitedWords.Add(word);
            JsonUtils.SaveListToJson(JsonPath, _prohibitedWords);

            return RedirectToAction("Index", "ProhibitedWords", new{ area = "Admin" });
        }

        // POST: ProhibitedWords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id < 0 || id >= _prohibitedWords.Count)
            {
                return HttpNotFound();
            }

            _prohibitedWords.RemoveAt(id);
            JsonUtils.SaveListToJson(JsonPath, _prohibitedWords);
            
            return RedirectToAction("Index");
        }
    }
}