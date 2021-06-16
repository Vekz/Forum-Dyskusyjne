using Forum_Dyskusyjne.DAL;
using Forum_Dyskusyjne.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Forum_Dyskusyjne.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {

        private ForumDbContext db = new ForumDbContext();

        // GET: Message
        public ActionResult Index(string UserID)
        {
            if (UserID != User.Identity.GetUserId())
            {
                return View("Error");
            }
            return View(new MessageViewModel(UserID));
        }


        //POST: Message
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendMessage(MakeMessageViewModel messageModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", new { UserID = User.Identity.GetUserId() });

            }
            User reciver = null;
            try
            { 
             reciver = db.Users.Where(u => u.UserName.Equals(messageModel.UserName)).Single();
            }
            catch(Exception ex)
            {
                if (ex is ArgumentNullException || ex is InvalidOperationException)
                {
                    ModelState.AddModelError("SendMessage", "No such user!");

                }
                else
                {
                    throw;
                }
            }
            User us = db.Users.Find(User.Identity.GetUserId());
            Message mess = new Message()
            {
                SendDate = DateTime.Now.ToLocalTime(),
                Sender = us,
                Receiver = reciver,
                Seen = false,
                Text = messageModel.Content,
                OrginalReciver = reciver.UserName,
                OrginalSender = us.UserName,
                Title = messageModel.Title,
            };

            db.Messages.Add(mess);
            await db.SaveChangesAsync();

            return RedirectToAction("Index", new { UserID = User.Identity.GetUserId() });

        }


        public async Task<ActionResult> Delete(int id)
        {
            Message message = db.Messages.Find(id);
            if(message != null)
            {
                if (message.ReceiverId == User.Identity.GetUserId())
                {
                    message.ReceiverId = db.Users.Where(u => u.UserName.Equals("This_user_doesnt_exist")).Single().Id;

                }else if(message.SenderId == User.Identity.GetUserId())
                {
                    message.SenderId = db.Users.Where(u => u.UserName.Equals("This_user_doesnt_exist")).Single().Id;

                }
            }   
            await db.SaveChangesAsync();

            return RedirectToAction("Index", new { UserID = User.Identity.GetUserId() });
        }

    }


   
}