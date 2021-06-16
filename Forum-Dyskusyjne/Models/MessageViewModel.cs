using Forum_Dyskusyjne.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum_Dyskusyjne.Models
{
    public class MessageViewModel
    {
        private string UserID = String.Empty;
        private ForumDbContext db = new ForumDbContext();

        public string GetUserID
        {
            get
            {
                return UserID;
            }
        }

        public MessageViewModel(string id)
        {
            UserID = id;
        }

        public List<Message> RecivedMessages
        {
            get
            {
                return db.Users.Find(UserID).MessagesReceived.ToList();
            }
        } 
        
        public List<Message> SentMessages
        {
            get
            {
                return db.Users.Find(UserID).MessagesSent.ToList();
            }
        }


    }
}