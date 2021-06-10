using System.ComponentModel.DataAnnotations;
using System.Web.WebPages;
using Forum_Dyskusyjne.Validators;
using Ganss.XSS;

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

        private string _content;
        [Required]
        [StringLength(500)]
        [AllowWords(ErrorMessage = "Post content contains disallowed words!")]
        [Display(Name="First post", Description = "First post")]
        public string FirstPostContent
        {
            get => _content;
            
            set
            {
                // Sanitizing html input with using https://github.com/mganss/HtmlSanitizer (which default rules are great)
                var sanitizer = new HtmlSanitizer();
                var body = sanitizer.Sanitize(value);
                if (body.IsEmpty())
                {
                    _content = null;
                }
                else
                {
                    _content = body;
                }
            }
        }
    }
}