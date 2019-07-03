using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SanalMarket.Startup))]
namespace SanalMarket
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
