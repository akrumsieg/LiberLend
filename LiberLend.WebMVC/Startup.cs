using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LiberLend.WebMVC.Startup))]
namespace LiberLend.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
