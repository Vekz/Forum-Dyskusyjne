using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum_Dyskusyjne.Models
{
    [Table("Thread")]
    public class Thread
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ThreadId { get; set; }

        [Required]
        public string ThreadTitle { get; set; } = String.Empty;

        public bool IsPinned { get; set; } = false;
        
        [Required]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public User Author { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        public ICollection<Post> Posts { get; set; }
    }
}