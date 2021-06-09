
using System.ComponentModel.DataAnnotations;

namespace Forum_Dyskusyjne.Models
{
    public class MakePostViewModel
    {
        [Required]
        [StringLength(500)]
        [Display(Name="Post Content", Description = "Post Content")]
        public string Content { get; set; }
        public int ThreadId { get; set; }
    }
}