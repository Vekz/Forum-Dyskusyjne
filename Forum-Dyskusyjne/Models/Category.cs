﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum_Dyskusyjne.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        [ForeignKey("Category")]
        public int ParentId { get; set; } 
        
        public ICollection<Category> SubCategories { get; set; }
        public ICollection<Thread> Threads { get; set; }
    }
}