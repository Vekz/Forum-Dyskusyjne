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
        }
    }
}
