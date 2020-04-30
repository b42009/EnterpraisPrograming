using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CACHIA_MIGUEL_EP.Startup))]
namespace CACHIA_MIGUEL_EP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
