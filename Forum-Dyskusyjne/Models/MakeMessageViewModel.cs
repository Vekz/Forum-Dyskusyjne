using Forum_Dyskusyjne.Areas.Admin.Controllers;
using Forum_Dyskusyjne.Areas.Utils;
using Forum_Dyskusyjne.DAL;
using Forum_Dyskusyjne.Validators;
using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Forum_Dyskusyjne.Models
{
    public class MakeMessageViewModel
    {
        private ForumDbContext db = new ForumDbContext();


        private string _content;

        private string _title;



        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Message Reciver", Description = "Message Reciver")]
        public string UserName
        {
            get; set;
        }


        [Required(AllowEmptyStrings = false)]
        [AllowHtml]
        [StringLength(500)]
        [AllowWords(ErrorMessage = "Message content contains disallowed words!")]
        [Display(Name = "Message Content", Description = "Message Content")]
        public  string Content
        {
            get => _content;

            set
            {
                string body = Sanitize(value);
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


        [Required(AllowEmptyStrings = false)]
        [AllowHtml]
        [StringLength(120)]
        [AllowWords(ErrorMessage = "Title contains disallowed words!")]
        [Display(Name = "Title", Description = "Title Content")]
        public string Title
        {
            get => _title;

            set
            {
                string body = Sanitize(value);
                if (body.IsEmpty())
                {
                    _title = null;
                }
                else
                {
                    _title = body;
                }
            }
        }


        private string Sanitize(string value)
        {
            // Sanitizing html input with using https://github.com/mganss/HtmlSanitizer (which default rules are great)
            var sanitizer = new HtmlSanitizer();
            sanitizer.AllowedTags.Clear();
            sanitizer.AllowedTags.UnionWith(JsonUtils.ReadStringListFromJson(AllowedTagsController.JsonPath));
            return sanitizer.Sanitize(value);
        }


    }
}