using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.WebPages;
using Forum_Dyskusyjne.Validators;
using Ganss.XSS;

namespace Forum_Dyskusyjne.Models
{
    public class MakePostViewModel
    {
        private string _content;
        [Required(AllowEmptyStrings=false)]
        [AllowHtml]
        [StringLength(500)]
        [AllowWords(ErrorMessage = "Post content contains disallowed words!")]
        [Display(Name="Post Content", Description = "Post Content")]
        public string Content
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
        
        public int ThreadId { get; set; }
    }
}