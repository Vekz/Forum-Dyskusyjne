using System.Linq;
using Forum_Dyskusyjne.Areas.Admin.Controllers;
using Forum_Dyskusyjne.Areas.Utils;
using Ganss.XSS;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Forum_Dyskusyjne.Startup))]
namespace Forum_Dyskusyjne
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
            // Set default allowed HTML tags
            // var defaultTags = HtmlSanitizer.DefaultAllowedTags.ToList();
            // JsonUtils.SaveListToJson(AllowedTagsController.JsonPath, defaultTags);
        }
    }
}
