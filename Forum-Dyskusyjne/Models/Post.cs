using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum_Dyskusyjne.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        
        [Required]
        [ForeignKey("Thread")]
        public int ThreadId { get; set; }
    }
}