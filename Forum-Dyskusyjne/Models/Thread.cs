using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum_Dyskusyjne.Models
{
    public class Thread
    {
        public int Thread_ID { get; set; }

        public string Thread_Title { get; set; }

        public int Author_Id { get; set; }

        public bool isPinned { get; set; }
       
        public int Category_ID { get; set; }
    }
}