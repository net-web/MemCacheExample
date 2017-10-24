using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FSOrderCenter.WebApp.Startup))]
namespace FSOrderCenter.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
