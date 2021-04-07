using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum_Dyskusyjne.Models
{
    [Table("Post")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual User Author { get; set; }
        
        [Required]
        [ForeignKey("Thread")]
        public int ThreadId { get; set; }
        public virtual Thread Thread { get; set; }
    }
}