using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum_Dyskusyjne.Models
{
    public class Message
    {
        //public int Message_ID { get; set; }

        public string Text { get; set; }
        public int Sender_ID { get; set; }
        public int Reciver_ID { get; set; }

        public DateTime sendDate { get; set; }

        public bool Seen { get; set; } = false ;


    }
}