using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum_Dyskusyjne.Models
{
    public class Post
    {
        public int Post_ID { get; set; }

        public string Body { get; set; }

        public int User_ID { get; set; }
        
        public int Thread_ID { get; set; }


    }
}