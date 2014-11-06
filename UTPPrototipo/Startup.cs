using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UTPPrototipo.Startup))]
namespace UTPPrototipo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
