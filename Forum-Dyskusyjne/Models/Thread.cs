using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum_Dyskusyjne.Models
{
    [Table("Thread")]
    public class Thread
    {
        public Thread()
        {
            CreatedTime = DateTime.UtcNow;
        }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ThreadId { get; set; }

        [Required]
        public string ThreadTitle { get; set; } = String.Empty;

        public bool IsPinned { get; set; } = false;
        
        [Required]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual User Author { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedTime { get; set; }
        
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}