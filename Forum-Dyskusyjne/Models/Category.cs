using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum_Dyskusyjne.Models
{
    public class Category
    {

        public int Category_ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Parent_ID { get; set; } 
    }
}