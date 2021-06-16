using Forum_Dyskusyjne.Areas.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Forum_Dyskusyjne.Areas.Admin.Controllers
{
    public class AnnouncementController : Controller
    {
        public static readonly string JsonPath = HostingEnvironment.MapPath("~/App_Data/announcement.json");
        string message  = JsonUtils.ReadStringFromJson(JsonPath);
        // GET: Admin/Announcement
        public ActionResult Index()
        {
            ViewBag.content = message;
            return View();
        }

        [HttpPost]
        public ActionResult Make(string word)
        {
            if (!ModelState.IsValid || String.IsNullOrWhiteSpace(word) || word.Any(Char.IsWhiteSpace))
            {
                ViewBag.content = message;
                return View("Index", message);
            }
            message = word;
            JsonUtils.SaveToJson(JsonPath, word);
            return  RedirectToAction("Index", "Announcement",  new { area = "Admin" });
        }
    }
}