using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum_Dyskusyjne.Models
{
    public class Thread
    {
        [Key]
        public int ThreadId { get; set; }

        [Required]
        public string ThreadTitle { get; set; }

        [Required]
        [ForeignKey("User")]
        public int AuthorId { get; set; }

        public bool IsPinned { get; set; } = false;
       
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        
        public ICollection<Post> Posts { get; set; }
    }
}