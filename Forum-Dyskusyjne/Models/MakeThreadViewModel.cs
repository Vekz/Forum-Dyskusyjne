using System.ComponentModel.DataAnnotations;

namespace Forum_Dyskusyjne.Models
{
    public class MakeThreadViewModel
    {
        [Required]
        [StringLength(40)]
        [Display(Name="Thread title", Description = "Thread title")]
        public string ThreadTitle { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        
        [Required]
        [StringLength(500)]
        [Display(Name="First post", Description = "First post")]
        public string FirstPostContent { get; set; }
    }
}